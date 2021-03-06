module mem_controller
    (
        input clk,
        input rst_n,
        mmio_if.user mmio,
        dma_if.peripheral dma,
        input rdy_tri,
        input patch_done,
        input term_cp,
        input [127:0] result[3:0], 
        output reg [1:0] we_mem[3:0],
        output [31:0] data_32,
        output [127:0] data_128,
        output reg cp_strt,
        output reg re_main,
        output reg wr_out_done,
        output [31:0] addr_main[3:0],
        output reg term
        );

    
    parameter NUM_THREAD = 32;
    parameter BIT_THREAD = $clog2(NUM_THREAD);
    parameter DMA_WRITE_SIZE = NUM_THREAD / 4;
    parameter DMA_WRITE_BIT = $clog2(DMA_WRITE_SIZE);



    logic mmio_system;
    assign mmio_system = mmio.wr_addr[10];


    logic term_cp_reg;
    always_ff @(posedge clk, negedge rst_n) begin
        if (!rst_n)
            term_cp_reg <= 1'h0;
        else if (term)
            term_cp_reg <= 1'h0;
        else if (term_cp)
            term_cp_reg <= 1'h1;
    end

    /*
        Read from Host
    */

    logic [63:0] dma_rd_addr_cp;
    logic [42:0] dma_rd_size_cp;
    logic dma_rd_req_cp;

    logic [63:0] dma_rd_addr_rt;
    logic [42:0] dma_rd_size_rt;
    logic dma_rd_req_rt;

    logic [63:0] dma_rd_addr_const;
    logic [42:0] dma_rd_size_const;
    logic dma_rd_req_const;

    logic [63:0] dma_rd_addr_tri;
    logic [42:0] dma_rd_size_tri;
    logic dma_rd_req_tri;

    logic dma_wr_addr_inc;
    logic [63:0] dma_wr_addr;

    logic [1:0] condition;
    logic dma_wr_done;

    // =============================================================//   
    // MMIO write code
    // =============================================================//     
    always_ff @(posedge clk, negedge rst_n) begin
        if (!rst_n) begin
            dma_rd_addr_cp <= '0;
            dma_rd_size_cp <= '0;
            dma_rd_req_cp <= '0;
            dma_rd_addr_rt <= '0;
            dma_rd_size_rt <= '0;
            dma_rd_req_rt <= '0;
            dma_rd_addr_const <= '0;
            dma_rd_size_const <= '0;
            dma_rd_req_const <= '0;
            dma_rd_addr_tri <= '0;
            dma_rd_size_tri <= '0;
            dma_rd_req_tri <= '0;
            dma_wr_addr <= '0;
            condition <= '0;
        end
        else if (mmio.wr_en == 1'h1) begin
            case(mmio.wr_addr)
                16'h0050: dma_rd_addr_cp <= mmio.wr_data[63:0];
                16'h0052: dma_rd_size_cp <= mmio.wr_data[42:0];
                16'h0054: dma_rd_req_cp <= mmio.wr_data[0];
                16'h0056: dma_rd_addr_rt <= mmio.wr_data[63:0];
                16'h0058: dma_rd_size_rt <= mmio.wr_data[42:0];
                16'h005A: dma_rd_req_rt <= mmio.wr_data[0];
                16'h005C: dma_rd_addr_const <= mmio.wr_data[63:0];
                16'h005E: dma_rd_size_const <= mmio.wr_data[42:0];
                16'h0060: dma_rd_req_const <= mmio.wr_data[0];
                16'h0062: dma_rd_addr_tri <= mmio.wr_data[63:0];
                16'h0064: dma_rd_size_tri <= mmio.wr_data[42:0];
                16'h0066: dma_rd_req_tri <= mmio.wr_data[0];
                16'h0068: dma_wr_addr <= mmio.wr_data[63:0];
                16'h006A: condition <= mmio.wr_data[1:0];
            endcase
        end
        else if (term)
            condition <= 2'b01;
        else if (dma_wr_done)
            condition <= 2'b10;
    end

     assign mmio.rd_data = {{62'h0}, condition};


    //DMA READ//

    logic dma_rd_go_cp, dma_rd_go_rt, dma_rd_go_const, dma_rd_go_tri;
    logic dma_rd_en_cp, dma_rd_en_rt, dma_rd_en_const, dma_rd_en_tri;
    logic [63:0] dma_rd_addr;
    logic [42:0] dma_rd_size;

    assign dma.rd_go = dma_rd_go_cp || dma_rd_go_rt || dma_rd_go_const || dma_rd_go_tri;
    assign dma.rd_en = dma_rd_en_cp || dma_rd_en_rt || dma_rd_en_const || dma_rd_en_tri;
    assign dma.rd_addr = dma_rd_addr;
    assign dma.rd_size = dma_rd_size;

    logic dma_rd_strt_cp, dma_rd_end_cp;
    logic dma_rd_strt_rt, dma_rd_end_rt;
    logic dma_rd_strt_const, dma_rd_end_const;
    logic dma_rd_strt_tri, dma_rd_end_tri;   

    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n) begin
            dma_rd_addr <= 64'h0;
            dma_rd_size <= 43'h0;
        end
        else if (dma_rd_strt_cp) begin
            dma_rd_addr <= dma_rd_addr_cp;
            dma_rd_size <= dma_rd_size_cp;
        end
        else if (dma_rd_strt_rt) begin
            dma_rd_addr <= dma_rd_addr_rt;
            dma_rd_size <= dma_rd_size_rt;
        end
        else if (dma_rd_strt_const) begin
            dma_rd_addr <= dma_rd_addr_const;
            dma_rd_size <= dma_rd_size_const;
        end
        else begin
            dma_rd_addr <= dma_rd_addr_tri;
            dma_rd_size <= dma_rd_size_tri;
        end
    end

    logic dma_rd_done;
    logic dma_rd_done_clr;
    always_ff @(posedge clk, negedge rst_n) begin
        if (!rst_n)
            dma_rd_done <= 1'h0;
        else if (dma_rd_done_clr)
            dma_rd_done <= 1'h0;
        else if (dma.rd_done)
            dma_rd_done <= 1'h1;
    end

    logic dma_rd_done_clr_cp, dma_rd_done_clr_rt, dma_rd_done_clr_const, dma_rd_done_clr_tri;
    
    assign dma_rd_done_clr = dma_rd_done_clr_cp || dma_rd_done_clr_rt 
                            || dma_rd_done_clr_const || dma_rd_done_clr_tri;

    // CP, RT, CONST

    logic [31:0] dma_rd_data_32[15:0];
    logic dma_rd_data_32_upd, dma_rd_data_32_shft;
    generate
        for (genvar i = 14; i >= 0; i = i - 1) begin
            always_ff @( posedge clk, negedge rst_n ) begin
                if (!rst_n)
                    dma_rd_data_32[i] <= 32'h0;
                else if (dma_rd_data_32_upd)
                    dma_rd_data_32[i] <= dma.rd_data[i*32+:32];
                else if (dma_rd_data_32_shft)
                    dma_rd_data_32[i] <= dma_rd_data_32[i+1];
            end
        end
    endgenerate
    
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            dma_rd_data_32[15] <= 32'h0;
        else if (dma_rd_data_32_upd)
            dma_rd_data_32[15] <= dma.rd_data[511:480];
    end
    assign data_32 = dma_rd_data_32[0];

    logic dma_rd_data_32_upd_cp, dma_rd_data_32_upd_rt, dma_rd_data_32_upd_const;
    logic dma_rd_data_32_shft_cp, dma_rd_data_32_shft_rt, dma_rd_data_32_shft_const;

    assign dma_rd_data_32_upd = dma_rd_data_32_upd_cp || dma_rd_data_32_upd_rt || dma_rd_data_32_upd_const;
    assign dma_rd_data_32_shft = dma_rd_data_32_shft_cp || dma_rd_data_32_shft_rt || dma_rd_data_32_shft_const;

    mem_controller_dma_rd_32 #(.SIZE_32(1)) dma_rd_cp 
                                        (.clk(clk),.rst_n(rst_n),
                                        .dma_rd_strt(dma_rd_strt_cp),
                                        .dma_rd_done(dma.rd_done),
                                        .dma_empty(dma.empty),
                                        .dma_rd_end_32(dma_rd_end_cp),
                                        .dma_rd_go_32(dma_rd_go_cp),
                                        .dma_rd_en_32(dma_rd_en_cp),
                                        .dma_rd_data_32_upd(dma_rd_data_32_upd_cp),
                                        .dma_rd_data_32_shft(dma_rd_data_32_shft_cp),
                                        .dma_rd_done_clr_32(dma_rd_done_clr_cp),
                                        .mem_wr_en_32(we_mem[0])
                                        );

    mem_controller_dma_rd_32 dma_rd_rt(.clk(clk),.rst_n(rst_n),
                                        .dma_rd_strt(dma_rd_strt_rt),
                                        .dma_rd_done(dma.rd_done),
                                        .dma_empty(dma.empty),
                                        .dma_rd_end_32(dma_rd_end_rt),
                                        .dma_rd_go_32(dma_rd_go_rt),
                                        .dma_rd_en_32(dma_rd_en_rt),
                                        .dma_rd_data_32_upd(dma_rd_data_32_upd_rt),
                                        .dma_rd_data_32_shft(dma_rd_data_32_shft_rt),
                                        .dma_rd_done_clr_32(dma_rd_done_clr_rt),
                                        .mem_wr_en_32(we_mem[1])
                                        );
                                        
    mem_controller_dma_rd_32 dma_rd_const(.clk(clk),.rst_n(rst_n),
                                        .dma_rd_strt(dma_rd_strt_const),
                                        .dma_rd_done(dma.rd_done),
                                        .dma_empty(dma.empty),
                                        .dma_rd_end_32(dma_rd_end_const),
                                        .dma_rd_go_32(dma_rd_go_const),
                                        .dma_rd_en_32(dma_rd_en_const),
                                        .dma_rd_data_32_upd(dma_rd_data_32_upd_const),
                                        .dma_rd_data_32_shft(dma_rd_data_32_shft_const),
                                        .dma_rd_done_clr_32(dma_rd_done_clr_const),
                                        .mem_wr_en_32(we_mem[2])
                                        );

    logic [31:0] dma_patch_size;
    always_ff @(posedge clk, negedge rst_n) begin
        if (!rst_n) 
            dma_patch_size <= 0;
        else if (dma_rd_data_32_shft_cp)
            dma_patch_size <= (dma_rd_data_32[0]%32) == 0 ? (dma_rd_data_32[0]/32) : (dma_rd_data_32[0]/32+1);
    end


    // TRI
    logic [127:0] dma_rd_data_128[3:0];
    logic dma_rd_data_128_upd, dma_rd_data_128_shft;
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n) begin
            dma_rd_data_128[0] <= 128'h0;
            dma_rd_data_128[1] <= 128'h0;
            dma_rd_data_128[2] <= 128'h0;
            dma_rd_data_128[3] <= 128'h0;
        end
        else if (dma_rd_data_128_upd) begin
            dma_rd_data_128[0] <= dma.rd_data[127:0];
            dma_rd_data_128[1] <= dma.rd_data[255:128];
            dma_rd_data_128[2] <= dma.rd_data[383:256];
            dma_rd_data_128[3] <= dma.rd_data[511:384];
        end
        else if (dma_rd_data_128_shft) begin
            dma_rd_data_128[0] <= dma_rd_data_128[1];
            dma_rd_data_128[1] <= dma_rd_data_128[2];
            dma_rd_data_128[2] <= dma_rd_data_128[3];
            dma_rd_data_128[3] <= dma_rd_data_128[3];
        end
    end

    assign data_128 = dma_rd_data_128[0];

    mem_controller_dma_rd_128 dma_rd_tri(.clk(clk),.rst_n(rst_n),
                                        .dma_rd_strt(dma_rd_strt_tri),
                                        .dma_rd_done(dma.rd_done),
                                        .dma_empty(dma.empty),
                                        .mem_wr_rdy(rdy_tri),
                                        .dma_rd_end_128(dma_rd_end_tri),
                                        .dma_rd_go_128(dma_rd_go_tri),
                                        .dma_rd_en_128(dma_rd_en_tri),
                                        .dma_rd_data_128_upd(dma_rd_data_128_upd),
                                        .dma_rd_data_128_shft(dma_rd_data_128_shft),
                                        .dma_rd_done_clr_128(dma_rd_done_clr_tri),
                                        .mem_wr_en_128(we_mem[3])
                                        );
    
    //Central Control Logic
    typedef enum reg [2:0] {DMA_RD_IDLE, DMA_RD_CP, DMA_RD_RT, DMA_RD_CONST, DMA_RD_TRI,
                            DMA_RD_CP_STRT, DMA_RD_DONE} t_state_dma_rd;
    t_state_dma_rd state_dma_rd, nxt_state_dma_rd;
    
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            state_dma_rd <= DMA_RD_IDLE;
        else
            state_dma_rd <= nxt_state_dma_rd;
    end

    logic cp_strt_reg;

    always_comb begin 
        nxt_state_dma_rd = DMA_RD_IDLE;

        dma_rd_strt_cp = 1'h0;
        dma_rd_strt_rt = 1'h0;
        dma_rd_strt_const = 1'h0;
        dma_rd_strt_tri = 1'h0;

        cp_strt_reg = 1'h0;

        case(state_dma_rd)
            DMA_RD_IDLE: begin
                if (condition[1] == 1'h1)
                    nxt_state_dma_rd = DMA_RD_CP;
            end
            DMA_RD_CP: begin
                if (dma_rd_req_cp && dma_rd_end_cp) begin
                    nxt_state_dma_rd = DMA_RD_RT; 
                end
                else if (dma_rd_req_cp) begin
                    nxt_state_dma_rd = DMA_RD_CP;
                    dma_rd_strt_cp = 1'h1;
                end
                else
                    nxt_state_dma_rd = DMA_RD_RT;
            end 
            DMA_RD_RT: begin
                if (dma_rd_req_rt && dma_rd_end_rt)
                    nxt_state_dma_rd = DMA_RD_CONST;
                else if (dma_rd_req_rt) begin
                    nxt_state_dma_rd = DMA_RD_RT;
                    dma_rd_strt_rt = 1'h1;
                end
                else
                    nxt_state_dma_rd = DMA_RD_CONST;
            end
            DMA_RD_CONST: begin
                if (dma_rd_req_const && dma_rd_end_const)
                    nxt_state_dma_rd = DMA_RD_TRI;
                else if (dma_rd_req_const) begin
                    nxt_state_dma_rd = DMA_RD_CONST;
                    dma_rd_strt_const = 1'h1;
                end
                else
                    nxt_state_dma_rd = DMA_RD_TRI;
            end
            DMA_RD_TRI: begin
                if (dma_rd_req_tri && dma_rd_end_tri) begin
                    nxt_state_dma_rd = DMA_RD_CP_STRT;
                end
                else if (dma_rd_req_tri) begin
                    nxt_state_dma_rd = DMA_RD_TRI;
                    dma_rd_strt_tri = 1'h1;
                end
                else begin
                    nxt_state_dma_rd = DMA_RD_CP_STRT; 
                end
            end
            DMA_RD_CP_STRT: begin
                nxt_state_dma_rd = DMA_RD_DONE;
                cp_strt_reg = 1'h1;
            end
            default: begin
                if (term == 1'h0)
                    nxt_state_dma_rd = DMA_RD_DONE;
            end
	endcase
    end


    always_ff @(posedge clk, negedge rst_n) begin
        if (!rst_n)
            cp_strt <= 1'h0;
        else 
            cp_strt <= cp_strt_reg;
    end

    /*
        Write to Host
    */

    // Patch Counter
    logic [31:0] dma_patch_cnt;
    logic dma_patch_inc;

    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            dma_patch_cnt <= '0;
        else if (term)
            dma_patch_cnt <= '0;
        else if (dma_patch_inc)
            dma_patch_cnt <= dma_patch_cnt + 1;
    end


    //DMA write
    logic dma_wr_go;
    logic dma_wr_en;
    logic [511:0] dma_wr_data;
    logic dma_wr_data_upd;

    assign dma.wr_go = dma_wr_go;
    assign dma.wr_data = dma_wr_data;
    assign dma.wr_addr = dma_wr_addr;
    assign dma.wr_size = DMA_WRITE_SIZE;
    assign dma.wr_en = dma_wr_en;

    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            dma_wr_data <= 512'h0;
        else if (dma_wr_data_upd)
            dma_wr_data <= {result[3], result[2], result[1], result[0]};
    end

    logic [2:0] mem_rd_cnt;
    logic mem_rd_clr, mem_rd_inc;
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            mem_rd_cnt <= 3'h0;
        else if (mem_rd_clr)
            mem_rd_cnt <= 3'h0;
        else if (mem_rd_inc)
            mem_rd_cnt <= mem_rd_cnt + 3'h1;
    end

    logic [DMA_WRITE_BIT:0] dma_write_cnt;
    logic dma_wr_clr, dma_wr_inc;
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n) 
            dma_write_cnt <= '0;
        else if (dma_wr_clr)
            dma_write_cnt <= '0;
        else if (dma_wr_inc)
            dma_write_cnt <= dma_write_cnt + 1;
    end

    logic [BIT_THREAD-1:0] thread_MC[3:0];
    logic thread_MC_clr, thread_MC_inc;
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n) begin
            thread_MC[0] <= {{(BIT_THREAD-2){1'h0}}, {2'h0}};
            thread_MC[1] <= {{(BIT_THREAD-2){1'h0}}, {2'h1}};
            thread_MC[2] <= {{(BIT_THREAD-2){1'h0}}, {2'h2}};
            thread_MC[3] <= {{(BIT_THREAD-2){1'h0}}, {2'h3}};
        end
        else if (thread_MC_clr) begin
            thread_MC[0] <= {{(BIT_THREAD-2){1'h0}}, {2'h0}};
            thread_MC[1] <= {{(BIT_THREAD-2){1'h0}}, {2'h1}};
            thread_MC[2] <= {{(BIT_THREAD-2){1'h0}}, {2'h2}};
            thread_MC[3] <= {{(BIT_THREAD-2){1'h0}}, {2'h3}};
        end
        else if (thread_MC_inc) begin
            thread_MC[0] <= thread_MC[0] + {{(BIT_THREAD-3){1'h0}}, {3'h4}};
            thread_MC[1] <= thread_MC[1] + {{(BIT_THREAD-3){1'h0}}, {3'h4}};
            thread_MC[2] <= thread_MC[2] + {{(BIT_THREAD-3){1'h0}}, {3'h4}};
            thread_MC[3] <= thread_MC[3] + {{(BIT_THREAD-3){1'h0}}, {3'h4}};
        end
    end
    assign addr_main[0] = {{(16-BIT_THREAD){1'h0}}, thread_MC[0], {16'hFFF0}};
    assign addr_main[1] = {{(16-BIT_THREAD){1'h0}}, thread_MC[1], {16'hFFF0}};
    assign addr_main[2] = {{(16-BIT_THREAD){1'h0}}, thread_MC[2], {16'hFFF0}};
    assign addr_main[3] = {{(16-BIT_THREAD){1'h0}}, thread_MC[3], {16'hFFF0}};
    
    typedef enum reg [2:0] {DMA_WR_IDLE, DMA_WR_WAIT, DMA_WR_HOLD, DMA_WR_LOAD, DMA_WR_DONE} t_state_DMA_wr;
    t_state_DMA_wr state_dma_wr, nxt_state_dma_wr;
    
    always_ff @( posedge clk, negedge rst_n ) begin
        if (!rst_n)
            state_dma_wr <= DMA_WR_IDLE;
        else
            state_dma_wr <= nxt_state_dma_wr;
    end

    always_comb begin
        nxt_state_dma_wr = DMA_WR_IDLE;
        dma_wr_go = 1'b0;
        re_main = 1'b0;
        dma_wr_en = 1'b0;
        dma_wr_data_upd = 1'b0;
        dma_wr_addr_inc = 1'b0;
        mem_rd_clr = 1'h0;
        mem_rd_inc = 1'h0;
        dma_wr_clr = 1'h0;
        dma_wr_inc = 1'h0;
        thread_MC_clr = 1'h0;
        thread_MC_inc = 1'h0;  
        dma_patch_inc = 1'h0; 
        term = 1'h0;
        dma_wr_done = 1'h0;

        case(state_dma_wr)
            DMA_WR_IDLE: begin
                if (patch_done) 
                    nxt_state_dma_wr = DMA_WR_WAIT;
            end
            DMA_WR_WAIT: begin
                if (condition[0] == 1'h1) begin
                    nxt_state_dma_wr = DMA_WR_LOAD;
                    dma_wr_go = 1'b1;
                    re_main = 1'b1;
                end 
                else 
                    nxt_state_dma_wr = DMA_WR_WAIT;
            end
            DMA_WR_LOAD: begin
                if (mem_rd_cnt == 3'h4) begin
                    nxt_state_dma_wr = DMA_WR_HOLD;
                    dma_wr_data_upd = 1'h1;
                    mem_rd_clr = 1'h1;
                    dma_wr_inc = 1'h1;
                    thread_MC_inc = 1'h1;
                end
                else begin
                    nxt_state_dma_wr = DMA_WR_LOAD;
                    mem_rd_inc = 1'h1;    
                end
            end
            DMA_WR_HOLD: begin
                if (!dma.full) begin
                    dma_wr_en = 1'h1;
                    re_main = 1'h1;
                    if (dma_write_cnt == DMA_WRITE_SIZE) begin
                        nxt_state_dma_wr = DMA_WR_DONE;
                        // dma_wr_addr_inc = 1'h1;
                        dma_wr_clr = 1'h1;
                        dma_patch_inc = 1'h1;
                        thread_MC_clr = 1'h1;
                    end
                    else begin
                        nxt_state_dma_wr = DMA_WR_LOAD;
                    end
                end
                else 
                    nxt_state_dma_wr = DMA_WR_HOLD;
            end
            default: begin
                if (dma.wr_done && term_cp_reg) begin
                    term = 1'h1; 
                    dma_wr_done = 1;
                end
                else if (dma.wr_done) begin
                    dma_wr_done = 1;
                end
                else 
                    nxt_state_dma_wr = DMA_WR_DONE;
            end
        endcase
    end
	 
	 always_ff @(posedge clk, negedge rst_n) begin
		if (!rst_n)
			wr_out_done <= 1'h0;
		else 
			wr_out_done <= dma_wr_done;
	 end
   

endmodule