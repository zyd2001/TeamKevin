`timescale 1ns / 1ps
module Calculation_tb();

  logic clk, rst;
  logic [31:0] sid_in;
  logic [95:0] v0, v1, v2, orig, dir;
  
  logic [31:0] sid_out;
  logic [95:0] Intersection_Point, norm_out;
  
  always #2 clk = ~clk;
  
  Calculation iDUT (
    .clk(clk), .rst(rst), 
    .v0(v0), .v1(v1), .v2(v2), .orig(orig), .dir(dir), .sid_in(sid_in),
    .sid_out(sid_out), .Intersection_Point(Intersection_Point), .norm_out(norm_out)
  );
  // 44
  // 1 0 1 0 1 0 1
  initial begin
    clk = 1'b0;
    rst = 1'b1;
    
    sid_in = 32'h00000007;
    
    v1   = 96'b01000000010000000000000000000000_01000000100000000000000000000000_00000000000000000000000000000000; //3,4,0
    v2   = 96'b11000000010000000000000000000000_01000000100000000000000000000000_00000000000000000000000000000000; //-3,4,0
    v0   = 96'b00000000000000000000000000000000_01000000100000000000000000000000_01000001001000000000000000000000; //0,4,10
    
    orig = 96'b00000000000000000000000000000000_00000000000000000000000000000000_01000000000000000000000000000000; //0,0,2
    dir  = 96'b00000000000000000000000000000000_00111111100000000000000000000000_00000000000000000000000000000000; //0,1,0
    
    #4
    rst = 1'b0;
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000100000000000000000000000_01000001000000000000000000000000; //3,4,8
    v2   = 96'b11000000010000000000000000000000_01000000100000000000000000000000_01000001000000000000000000000000; //-3,4,8
    v0   = 96'b00000000000000000000000000000000_01000000100000000000000000000000_01000001001000000000000000000000; //0,4,10
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000000000000000000000000000_00000000000000000000000000000000; //3,2,0
    v2   = 96'b11000000010000000000000000000000_01000000000000000000000000000000_00000000000000000000000000000000; //-3,2,0
    v0   = 96'b00000000000000000000000000000000_01000000000000000000000000000000_01000001001000000000000000000000; //0,2,10
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000000000000000000000000000_01000000100000000000000000000000; //3,2,4
    v2   = 96'b11000000010000000000000000000000_01000000000000000000000000000000_01000000100000000000000000000000; //-3,2,4
    v0   = 96'b00000000000000000000000000000000_01000000000000000000000000000000_01000001001000000000000000000000; //0,2,10
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000000000000000000000000000_11000000010000000000000000000000; //3,2,-3
    v2   = 96'b11000000010000000000000000000000_01000000000000000000000000000000_11000000010000000000000000000000; //-3,2,-3
    v0   = 96'b00000000000000000000000000000000_01000000000000000000000000000000_01000001001000000000000000000000; //0,2,10
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000000000000000000000000000_01000000110000000000000000000000; //3,2,6
    v2   = 96'b11000000010000000000000000000000_01000000000000000000000000000000_01000000110000000000000000000000; //-3,2,6
    v0   = 96'b00000000000000000000000000000000_01000000000000000000000000000000_01000001001000000000000000000000; //0,2,10
    
    #176
    v1   = 96'b01000000010000000000000000000000_01000000000000000000000000000000_10111111100000000000000000000000; //3,2,-1
    v2   = 96'b11000000010000000000000000000000_01000000000000000000000000000000_10111111100000000000000000000000; //-3,2,-1
    v0   = 96'b00000000000000000000000000000000_01000000000000000000000000000000_01000001001000000000000000000000; //0,2,10

  end
  
endmodule