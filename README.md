# ECE554-RayTracing-TeamKEVIN

# How to run simulation on AFU_ASE

`git clone` this repository

open 2 terminals, `~/opae/bin/tool_setup.sh` in both terminals

## Hardware terminal

`cd Hardware`

`./auto.sh`
This script will compile system verilog files and start simulation.

## Software terminal

`cd Software/Host`

There are many files in this folder

+ `main.kpp` the software source code that will run on the core
+ `main.asm` the software assembly code compiled by our compiler
+ `Assembler.py` the assembler
+ `RT.isa` ISA specification file used by the assembler
+ `box.binary` a sample triangles binary file for the image in the report
+ `CP.binary` specify how many pixels to render
+ `constant.binary` binary for constant memory

all these `.binary` files can be generated by a series of python scripts in Software/Assembler folder. There will be a README file that explain all the scripts

Since the compiler is written in C# and if you don't have .Net SDK installed, I have a link for the stand-alone compiler executable (for linux). https://uwmadison.box.com/s/4i22j2tx7aves5ogfz65plreysjjr908. To compile, run `./CompilerCore main.kpp > main.asm`.
For convinence, I also include the compiled result (`main.asm` file).

`./Assembler.py -r main.asm` or `python3 ./Assembler.py -r main.asm` to assemble the assembly

`make` to build the host software.

After hardware simulation start, make sure to set the `ASE_WORKDIR` according to the Hardware terminal's prompt.

`ASE_LOG=0 ./afu_ase` to run. Several `get` and a `break` will be printed in a while. Then the output image will be written to `output.ppm`. The example here is at `16×12` resolution (for faster simulation).

