// Copyright (c) 2020 University of Florida
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

// Greg Stitt
// University of Florida
//
// Description: This application demonstrates a DMA AFU where the FPGA transfers
// data from an input array into an output array.
//
// The example demonstrates an extension of the AFU wrapper class that uses
// AFU::malloc() to dynamically allocate virtually contiguous memory that can
// be accessed by both software and the AFU.

// INSTRUCTIONS: Change the configuration settings in config.h to test
// different types of data.

#include <cstdlib>
#include <iostream>
#include <cmath>
#include <fstream>
#include <stdio.h>

#include <opae/utils.h>

#include "AFU.h"
// Contains application-specific information
#include "config.h"
// Auto-generated by OPAE's afu_json_mgr script
#include "afu_json_info.h"
#include <string>
#include <cinttypes>
#define STB_IMAGE_WRITE_IMPLEMENTATION
#include "stb_image_write.h"

using namespace std;

int width, height;

void clearMem(dma_data_t *mem, int size)
{
    for (int i = 0; i < size; i++)
        mem[i] = 0;
}

void readFile(dma_data_t *data, ifstream &f)
{
    char ch;
    while (true)
    {
        f.read(&ch, 1);
        if (f.eof())
            break;
        *data = ch;
        data++;
    }
}

ifstream::pos_type filesize(ifstream &f)
{
    f.seekg(0, f.end);
    auto size = f.tellg();
    f.seekg(0, f.beg);
    return size;
}

void getOutput(dma_data_t *data, uint8_t * buffer)
{
    volatile float *ptr = (volatile float *)data;
    for (int i = 0; i < OUTPUT_SIZE / 4; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            buffer[i * 3 + j] = pow(*ptr, 1 / 2.2) * 255;
            ptr++;
        }
        ptr++;
    }
}

void outputImage(uint8_t * buffer, int i)
{   
    string str;
    str = "out/output";
    str += to_string(i);
    stbi_write_png(str.c_str(), width, height, 3, buffer, width * 3);
}

void run(AFU &afu, dma_data_t *data, uint8_t * buffer, int i)
{
    afu.write(CONDITION, (uint64_t)3);
    uint8_t * internal_buffer = buffer;
    while (true)
    {
        auto ret = afu.read(CONDITION);
        bool patch_done = !(ret & 1);
        bool all_done = !(ret & 2);
        if (patch_done)
        {
            // fprintf(stderr, "get\n");
            getOutput(data, internal_buffer);
            internal_buffer += 32 * 3;
            afu.write(CONDITION, 3);
        }
        if (all_done)
        {
            getOutput(data, internal_buffer);
            cout << "done " << i <<"\n";
            outputImage(buffer, i);
            break;
        }
    }
}

void changeConstant(dma_data_t *data, float value, int offset)
{
    data += offset;
    volatile float *ptr = (volatile float *)data;
    *ptr = value;
}

void changeConstantInt(dma_data_t *data, int value, int offset)
{
    data += offset;
    volatile int *ptr = (volatile int *)data;
    *ptr = value;
}

int main(int argc, char *argv[])
{
    try
    {
        // Create an AFU object to provide basic services for the FPGA. The
        // constructor searchers available FPGAs for one with an AFU with the
        // the specified ID
        AFU afu(AFU_ACCEL_UUID);
        bool failed = false;

        ifstream rt("main.asm.out", ios::binary), con("constant.binary", ios::binary), tri("box.binary", ios::binary);

        width = atoi(argv[1]);
        height = atoi(argv[2]);
        uint8_t * buffer = new uint8_t[width * height * 3];

        cout << "Starting...\n";

        unsigned int CPInsSize, RTInsSize, constantSize, triangleSize;
        unsigned int CPInsFileSize, RTInsFileSize, constantFileSize, triangleFileSize;
        CPInsFileSize, RTInsFileSize, constantFileSize, triangleFileSize;
        CPInsSize = 4;
        CPInsFileSize = CPInsSize;
        RTInsSize = filesize(rt);
        RTInsFileSize = RTInsSize;
        constantSize = filesize(con);
        constantFileSize = constantSize;
        triangleSize = filesize(tri);
        triangleFileSize = triangleSize;
        CPInsSize = CPInsSize - CPInsSize % 64 + 64;
        RTInsSize = RTInsSize - RTInsSize % 64 + 64;
        constantSize = constantSize - constantSize % 64 + 64;
        triangleSize = triangleSize - triangleSize % 64 + 64;

        auto CPIns = afu.malloc<dma_data_t>(CPInsSize);
        auto RTIns = afu.malloc<dma_data_t>(RTInsSize);
        auto constant = afu.malloc<dma_data_t>(constantSize);
        auto triangle = afu.malloc<dma_data_t>(triangleSize);
        auto output = afu.malloc<dma_data_t>(OUTPUT_SIZE * 4);

        printf("CP: %d\nRT: %d\nCon: %d\nTri: %d\n", CPInsSize, RTInsSize, constantSize, triangleSize);
        printf("CP: %p\nRT: %p\nCon: %p\nTri: %p\nOut: %p\n", CPIns, RTIns, constant, triangle, output);

        int CPindex = 0;

        clearMem(CPIns, CPInsSize);
        clearMem(RTIns, RTInsSize);
        clearMem(constant, constantSize);
        clearMem(triangle, triangleSize);
        clearMem(output, OUTPUT_SIZE * 4);

        *((int*)CPIns) = width * height;
        readFile(RTIns, rt);
        readFile(constant, con);
        readFile(triangle, tri);
        
        changeConstantInt(constant, width, 0);
        changeConstantInt(constant, height, 4);
        changeConstant(constant, -3.0, 16);
        
        // Inform the FPGA of the starting read and write address of the arrays.
        afu.write(CP_ADDR, (uint64_t)CPIns);
        afu.write(CP_SIZE, (uint64_t)1);
        afu.write(CP_LOAD, (uint64_t)1);
        afu.write(RT_ADDR, (uint64_t)RTIns);
        afu.write(RT_SIZE, (uint64_t)(RTInsSize / AFU::CL_BYTES));
        afu.write(RT_LOAD, (uint64_t)1);
        afu.write(CON_ADDR, (uint64_t)constant);
        afu.write(CON_SIZE, (uint64_t)(constantSize / AFU::CL_BYTES));
        afu.write(CON_LOAD, (uint64_t)1);
        afu.write(TRI_ADDR, (uint64_t)triangle);
        afu.write(TRI_SIZE, (uint64_t)(triangleSize / AFU::CL_BYTES));
        afu.write(TRI_LOAD, (uint64_t)1);
        afu.write(OUT_ADDR, (uint64_t)output);
        run(afu, output, buffer, 0);

        // The FPGA DMA only handles cache-line transfers, so we need to convert
        // the array size to cache lines.
        afu.write(CP_LOAD, (uint64_t)0);
        afu.write(RT_LOAD, (uint64_t)0);
        afu.write(CON_LOAD, (uint64_t)0);
        afu.write(TRI_LOAD, (uint64_t)0);

        for (int i = 1; i < 24 * 5; i++)
        {
            changeConstant(constant, -3.0 + i * 6.0 / 120, 16);
            afu.write(CON_LOAD, 1);
            run(afu, output, buffer, i);
        }

        system("cd out && ffmpeg -r 24 -f image2 -s 480x360 -i ./output%d -vcodec libx264 -crf 25 -pix_fmt yuv420p output.mp4");

        delete[] buffer;
        // Free the allocated memory.
        afu.free(CPIns);
        afu.free(RTIns);
        afu.free(constant);
        afu.free(triangle);
        afu.free(output);
        // }
        return EXIT_SUCCESS;
    }
    // Exception handling for all the runtime errors that can occur within
    // the AFU wrapper class.
    catch (const fpga_result &e)
    {

        // Provide more meaningful error messages for each exception.
        if (e == FPGA_BUSY)
        {
            cerr << "ERROR: All FPGAs busy." << endl;
        }
        else if (e == FPGA_NOT_FOUND)
        {
            cerr << "ERROR: FPGA with accelerator " << AFU_ACCEL_UUID
                 << " not found." << endl;
        }
        else
        {
            // Print the default error string for the remaining fpga_result types.
            cerr << "ERROR: " << fpgaErrStr(e) << endl;
        }
    }
    catch (const runtime_error &e)
    {
        cerr << e.what() << endl;
    }
    catch (const opae::fpga::types::no_driver &e)
    {
        cerr << "ERROR: No FPGA driver found." << endl;
    }

    return EXIT_FAILURE;
}