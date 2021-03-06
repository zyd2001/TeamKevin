module mem_main_random_conflict_tb();
    
    parameter NUM_RT = 4;
    parameter NUM_THREAD = 32;
    parameter NUM_BANK_PTHREAD = 4;
    localparam TESTS = 40000;
    localparam TEST_CYCLE = 100;
    localparam ADDRESS_DEPTH_PBANK = 12;
    localparam ADDRESS_BIT = $clog2(NUM_THREAD)+ADDRESS_DEPTH_PBANK+2;

    logic clk;
    logic rst_n;
    logic we_RT[NUM_RT-1:0];
    logic re_RT[NUM_RT-1:0];
    logic mode[NUM_RT-1:0];
    logic [31:0] addr_RT[NUM_RT-1:0];
    logic [ADDRESS_BIT-1:0] addr_RT_rand[NUM_RT-1:0];
    

    logic [127:0] data_RT_in[NUM_RT-1:0];


    logic rd_rdy_RT[NUM_RT-1:0];
    logic [127:0] data_RT_out[NUM_RT-1:0];

    always #1 clk = ~clk;


    mem_main #(NUM_RT, NUM_THREAD, NUM_BANK_PTHREAD) main
                   (.clk(clk),
                    .rst_n(rst_n),
                    .we(we_RT),
                    .re(re_RT),
                    .addr(addr_RT),
                    .data_in(data_RT_in),
                    .data_out(data_RT_out),
                    .rd_rdy(rd_rdy_RT),
                    .mode(mode)
                    );
    
    logic [127:0] data_RT_copy[NUM_RT+1:0];
    logic [31:0] addr_RT_copy[NUM_RT+1:0];
    logic we_RT_copy[NUM_RT+1:0];
    logic RT_busy[NUM_RT-1:0];
    int error = 0;
    int test_count = 0;
    logic retry = 1'b0;
    int en_ctn = 0;
    initial begin
        for(int test = 0; test < TESTS; test++) begin
            $display("Test %d starting... ", test);
            clk = 1;
            rst_n = 1;
            we_RT = '{NUM_RT{1'b0}};
            re_RT = '{NUM_RT{1'b0}};
            RT_busy = '{NUM_RT{1'b0}};
            mode = '{NUM_RT{1'b1}};
            for(int i=0;i<NUM_RT;++i) begin
                addr_RT[i] = 32'b0;
                data_RT_in[i] = 128'b0;
            end

            // reset
            @(posedge clk) begin end
            rst_n = 1'b0; // active low reset
            @(posedge clk) begin end
            rst_n = 1'b1; // reset finished
            @(posedge clk) begin end
            // test on RT
            en_ctn = (en_ctn + 1) % NUM_RT;
            we_RT = '{NUM_RT{1'b0}};
            we_RT[en_ctn] = 1'b1;
            for(int k = 0; k < TEST_CYCLE; k++) begin
                if(k%(TEST_CYCLE/10)==0)begin
                    $display("Current test cycle: %d", k);
                end
                // test foreach core
                for(int i = NUM_RT-1; i >= 0; i--) begin
                    if(!RT_busy[i]) begin
                        RT_busy[i] = 1'b1;
                        data_RT_in[i] = {i,i,i,i};
                        addr_RT_rand[i] = 0;
                        addr_RT[i] = {{(30-ADDRESS_BIT){1'b0}}, addr_RT_rand[i], 2'b0};
                    end
                    if(we_RT[i] & !re_RT[i]) begin
                        data_RT_copy[i+2] = data_RT_in[i];
                        addr_RT_copy[i+2] = addr_RT[i];
                        we_RT_copy[i+2] = we_RT[i];
                        if(i == 1) begin
                            data_RT_copy[1] = data_RT_copy[NUM_RT+1];
                            addr_RT_copy[1] = addr_RT_copy[NUM_RT+1];
                            we_RT_copy[1] = we_RT_copy[NUM_RT+1];
                        end
                        if(i == 0) begin
                            data_RT_copy[0] = data_RT_copy[NUM_RT];
                            addr_RT_copy[0] = addr_RT_copy[NUM_RT];
                            we_RT_copy[0] = we_RT_copy[NUM_RT];
                        end
                    end
                end
                @(posedge clk);
                @(negedge clk);
                // test read
                for(int j = 0; j < NUM_RT; j++)begin
                    if(re_RT[j]) begin
                        re_RT[j] = 1'b0;
                    end
                    else if(we_RT[j]) begin
                        re_RT[j] = we_RT_copy[j];
                        we_RT[j] = 0;
                        addr_RT[j] = addr_RT_copy[j];
                    end
                    if(rd_rdy_RT[j]) begin
                        $stop;
                        test_count ++;
                        if(data_RT_copy[j] !== data_RT_out[j]) begin
                            $display("Error! At cycle: %d, Failed R at address: 0x%h, port %d, data expecting: 0x%h, data got: 0x%h", 
                                    k, addr_RT_copy[j], j, data_RT_copy[j], data_RT_out[j]); 
                            error ++;
                            $stop();
                        end
                        RT_busy[j] = 0;
                    end
                end
            end
        end
        if(test_count == 0) begin
            $display("Error! <%d> test has been run", test_count);
        end 
        else if(error == 0) begin
            $display("All <%d> tests passed!!!", test_count); 
        end
        else begin
            $display("In all <%d> tests, there are <%d> errors", test_count, error);
        end
        $stop();
    end
endmodule