module TriManager_v3(
  clk, rst,
  Mem_Rdy, Fetch,
  v1_in, v1_out,
  v2_in, v2_out,
  v0_in, v0_out,
  sid_in, sid_out,
  Mem_NotValid, Mem_En,
  triangle_id);

  parameter NUM_TRIANGLE = 512;
  localparam BIT_TRIANGLE = $clog2(NUM_TRIANGLE);
  
  input clk, rst;
  input Mem_Rdy, Fetch;
  input [95:0] v1_in, v2_in, v0_in;
  input [31:0] sid_in;
  input Mem_NotValid;

  output Mem_En;
  output [95:0] v1_out, v2_out, v0_out;
  output [31:0] sid_out;
  output unsigned [BIT_TRIANGLE-1:0] triangle_id;

  typedef enum reg {FTCH, IDLE} state_t;
  state_t state, nxt_state;
  
  logic ld, Mem_En_in, clear;
  logic [95:0] v1_reg, v2_reg, v0_reg;
  logic [31:0] sid_reg;
  logic unsigned [BIT_TRIANGLE-1:0] triangle_id_reg;

  always_ff@(posedge clk or posedge rst) begin
    if (rst)
      state <= IDLE;
    else 
      state <= nxt_state;
  end
  
  always@(posedge clk or posedge rst) begin
    if (rst|clear) begin
      v1_reg <= '0;
      v2_reg <= '0;
      v0_reg <= '0;
      sid_reg <= '0;
      triangle_id_reg <= '0;
    end
    else if (ld) begin
      v1_reg <= v1_in;
      v2_reg <= v2_in;
      v0_reg <= v0_in;
      sid_reg <= sid_in;
      triangle_id_reg <= triangle_id_reg + 1'b1;
    end
  end

  always_comb begin
    Mem_En_in = 1'b0;
    clear = 1'b0;
    ld = 1'b0;
    nxt_state = IDLE;
    case(state)
      FTCH: 
        begin
          if (Mem_NotValid)
            clear = 1'b1;
          else if (Mem_Rdy)
            ld = 1'b1;
          else 
            nxt_state = FTCH;
        end
      default: 
        begin
          if (Fetch) begin
            Mem_En_in = 1'b1;
            nxt_state = FTCH;
          end
        end
    endcase
  end  

  assign Mem_En = Mem_En_in;
  assign triangle_id = triangle_id_reg;
  assign v1_out = v1_reg;
  assign v2_out = v2_reg;
  assign v0_out = v0_reg;
  assign sid_out = sid_reg;

endmodule
