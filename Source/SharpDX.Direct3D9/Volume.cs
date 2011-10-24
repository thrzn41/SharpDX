﻿// Copyright (c) 2010-2011 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;

using SharpDX.Direct3D;

namespace SharpDX.Direct3D9
{
    public partial class Volume
    {
        /// <summary>
        /// Loads a volume from a file on the disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileW([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] const wchar_t* pSrcFile,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey,[In] D3DXIMAGE_INFO* pSrcInfo)</unmanaged>
        public static Result FromFile(Volume volume, string fileName, Filter filter, int colorKey)
        {
            return D3DX9.LoadVolumeFromFileW(volume, null, IntPtr.Zero, fileName, IntPtr.Zero, filter, colorKey, IntPtr.Zero);
        }

        /// <summary>
        /// Loads a volume from a file on the disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileW([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] const wchar_t* pSrcFile,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey,[In] D3DXIMAGE_INFO* pSrcInfo)</unmanaged>
        public static Result FromFile(Volume volume, string fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox)
        {
            unsafe
            {
                return D3DX9.LoadVolumeFromFileW(volume, null, new IntPtr(&destinationBox), fileName, new IntPtr(&sourceBox), filter, colorKey, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Loads a volume from a file on the disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileW([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] const wchar_t* pSrcFile,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey,[In] D3DXIMAGE_INFO* pSrcInfo)</unmanaged>
        public static Result FromFile(Volume volume, string fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox, out ImageInformation imageInformation)
        {

            return FromFile(volume, fileName, filter, colorKey, sourceBox, destinationBox, null, out imageInformation);
        }

        /// <summary>
        /// Loads a volume from a file on the disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="palette">The palette.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileW([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] const wchar_t* pSrcFile,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey,[In] D3DXIMAGE_INFO* pSrcInfo)</unmanaged>
        public static Result FromFile(Volume volume, string fileName, Filter filter, int colorKey, Box sourceBox, Box destinationBox, PaletteEntry[] palette, out ImageInformation imageInformation)
        {
            unsafe
            {
                fixed (void* pImageInformation = &imageInformation)
                    return D3DX9.LoadVolumeFromFileW(volume, palette, new IntPtr(&destinationBox), fileName, new IntPtr(&sourceBox), filter, colorKey, (IntPtr)pImageInformation);
            }
        }

        /// <summary>
        /// Loads a volume from a file in memory.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="memory">The memory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInMemory(Volume volume, byte[] memory, Filter filter, int colorKey)
        {
            unsafe
            {
                fixed (void* pMemory = memory)
                    return D3DX9.LoadVolumeFromFileInMemory(volume, null, IntPtr.Zero, (IntPtr)pMemory, memory.Length, IntPtr.Zero, filter, colorKey, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Loads a volume from a file in memory.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="memory">The memory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInMemory(Volume volume, byte[] memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox)
        {
            unsafe
            {
                fixed (void* pMemory = memory)
                    return D3DX9.LoadVolumeFromFileInMemory(volume, null, new IntPtr(&destinationBox), (IntPtr)pMemory, memory.Length, new IntPtr(&sourceBox), filter, colorKey, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Loads a volume from a file in memory.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="memory">The memory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInMemory(Volume volume, byte[] memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox, out ImageInformation imageInformation)
        {
            return FromFileInMemory(volume, memory, filter, colorKey, sourceBox, destinationBox, null, out imageInformation);
        }

        /// <summary>
        /// Loads a volume from a file in memory.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="memory">The memory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="palette">The palette.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInMemory(Volume volume, byte[] memory, Filter filter, int colorKey, Box sourceBox, Box destinationBox, PaletteEntry[] palette, out ImageInformation imageInformation)
        {
            unsafe
            {
                fixed (void* pMemory = memory)
                    fixed (void* pImageInformation = &imageInformation)
                        return D3DX9.LoadVolumeFromFileInMemory(volume, palette, new IntPtr(&destinationBox), (IntPtr)pMemory, memory.Length, new IntPtr(&sourceBox), filter, colorKey, (IntPtr)pImageInformation);
            }
        }

        /// <summary>
        /// Loads a volume from a file in a strean.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInStream(Volume volume, Stream stream, Filter filter, int colorKey)
        {

            return CreateFromFileInStream(volume, stream, filter, colorKey, IntPtr.Zero, IntPtr.Zero, null, IntPtr.Zero);
        }

        /// <summary>
        /// Loads a volume from a file in a strean.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInStream(Volume volume, Stream stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox)
        {
            unsafe
            {
                return CreateFromFileInStream(volume, stream, filter, colorKey, new IntPtr(&sourceBox), new IntPtr(&destinationBox), null, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Loads a volume from a file in a strean.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInStream(Volume volume, Stream stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox, out ImageInformation imageInformation)
        {

            return FromFileInStream(volume, stream, filter, colorKey, sourceBox, destinationBox, null, out imageInformation);
        }

        /// <summary>
        /// Loads a volume from a file in a strean.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="palette">The palette.</param>
        /// <param name="imageInformation">The image information.</param>
        /// <returns>A <see cref="SharpDX.Result" /> object describing the result of the operation.</returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromFileInMemory([In] IDirect3DVolume9* pDestVolume,[Out, Buffer] const PALETTEENTRY* pDestPalette,[In] const void* pDestBox,[In] const void* pSrcData,[In] unsigned int SrcDataSize,[In] const void* pSrcBox,[In] D3DX_FILTER Filter,[In] int ColorKey,[In] void* pSrcInfo)</unmanaged>
        public static Result FromFileInStream(Volume volume, Stream stream, Filter filter, int colorKey, Box sourceBox, Box destinationBox, PaletteEntry[] palette, out ImageInformation imageInformation)
        {
            unsafe
            {
                fixed (void* pImageInformation = &imageInformation)
                    return CreateFromFileInStream(volume, stream, filter, colorKey, new IntPtr(&sourceBox), new IntPtr(&destinationBox), palette, (IntPtr)pImageInformation);
            }
        }

        private static Result CreateFromFileInStream(Volume volume, Stream stream, Filter filter, int colorKey, IntPtr sourceBox, IntPtr destinationBox, PaletteEntry[] palette, IntPtr imageInformation)
        {

            unsafe
            {
                if (stream is DataStream)
                    return D3DX9.LoadVolumeFromFileInMemory(volume, palette, destinationBox, ((DataStream)stream).DataPointer, (int)stream.Length, sourceBox, filter, colorKey, (IntPtr)imageInformation);
                var data = Utilities.ReadStream(stream);
                fixed (void* pData = data)
                    return D3DX9.LoadVolumeFromFileInMemory(volume, palette, destinationBox, (IntPtr)pData, data.Length, sourceBox, filter, colorKey, (IntPtr)imageInformation);
            }
        }

        /// <summary>
        /// Loads a volume from a source volume.
        /// </summary>
        /// <param name="destinationVolume">The destination volume.</param>
        /// <param name="sourceVolume">The source volume.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromVolume([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey)</unmanaged>
        public static Result FromVolume(Volume destinationVolume, Volume sourceVolume, Filter filter, int colorKey)
        {
            return D3DX9.LoadVolumeFromVolume(destinationVolume, null, IntPtr.Zero, sourceVolume, null, IntPtr.Zero, filter, colorKey);
        }

        /// <summary>
        /// Loads a volume from a source volume.
        /// </summary>
        /// <param name="destinationVolume">The destination volume.</param>
        /// <param name="sourceVolume">The source volume.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromVolume([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey)</unmanaged>
        public static Result FromVolume(Volume destinationVolume, Volume sourceVolume, Filter filter, int colorKey, Box sourceBox, Box destinationBox)
        {
            return FromVolume(destinationVolume, sourceVolume, filter, colorKey, sourceBox, destinationBox, null, null);
        }

        /// <summary>
        /// Loads a volume from a source volume.
        /// </summary>
        /// <param name="destinationVolume">The destination volume.</param>
        /// <param name="sourceVolume">The source volume.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="colorKey">The color key.</param>
        /// <param name="sourceBox">The source box.</param>
        /// <param name="destinationBox">The destination box.</param>
        /// <param name="destinationPalette">The destination palette.</param>
        /// <param name="sourcePalette">The source palette.</param>
        /// <returns>A <see cref="SharpDX.Result" /> object describing the result of the operation.</returns>
        /// <unmanaged>HRESULT D3DXLoadVolumeFromVolume([In] IDirect3DVolume9* pDestVolume,[In] const PALETTEENTRY* pDestPalette,[In] const D3DBOX* pDestBox,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox,[In] unsigned int Filter,[In] D3DCOLOR ColorKey)</unmanaged>
        public static Result FromVolume(Volume destinationVolume, Volume sourceVolume, Filter filter, int colorKey, Box sourceBox, Box destinationBox, PaletteEntry[] destinationPalette, PaletteEntry[] sourcePalette)
        {
            unsafe
            {
                return D3DX9.LoadVolumeFromVolume(destinationVolume, destinationPalette, new IntPtr(&destinationBox), sourceVolume, sourcePalette, new IntPtr(&sourceBox), filter, colorKey);
            }
        }

        /// <summary>
        /// Locks a box on a volume resource.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <returns>
        /// The locked region of this resource
        /// </returns>
        /// <unmanaged>HRESULT IDirect3DVolume9::LockBox([Out] D3DLOCKED_BOX* pLockedVolume,[In] const void* pBox,[In] D3DLOCK Flags)</unmanaged>
        public DataBox LockBox(LockFlags flags)
        {

            LockedBox lockedBox;
            LockBox(out lockedBox, IntPtr.Zero, flags);
            return new DataBox(lockedBox.PBits, lockedBox.RowPitch, lockedBox.SlicePitch);
        }

        /// <summary>
        /// Locks a box on a volume resource.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The locked region of this resource</returns>
        /// <unmanaged>HRESULT IDirect3DVolume9::LockBox([Out] D3DLOCKED_BOX* pLockedVolume,[In] const void* pBox,[In] D3DLOCK Flags)</unmanaged>
        public DataBox LockBox(Box box, LockFlags flags)
        {
            unsafe
            {
                LockedBox lockedBox;
                LockBox(out lockedBox, new IntPtr(&box), flags);
                return new DataBox(lockedBox.PBits, lockedBox.RowPitch, lockedBox.SlicePitch);
            }
        }

        /// <summary>
        /// Saves a volume to a file on disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileW([In] const wchar_t* pDestFile,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox)</unmanaged>
        public static Result ToFile(Volume volume, string fileName, ImageFileFormat format)
        {
            return D3DX9.SaveVolumeToFileW(fileName, format, volume, null, IntPtr.Zero);
        }

        /// <summary>
        /// Saves a volume to a file on disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <param name="box">The box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileW([In] const wchar_t* pDestFile,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox)</unmanaged>
        public static Result ToFile(Volume volume, string fileName, ImageFileFormat format, Box box)
        {
            return ToFile(volume, fileName, format, box, null);
        }

        /// <summary>
        /// Saves a volume to a file on disk.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <param name="box">The box.</param>
        /// <param name="palette">The palette.</param>
        /// <returns>A <see cref="SharpDX.Result" /> object describing the result of the operation.</returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileW([In] const wchar_t* pDestFile,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In] const PALETTEENTRY* pSrcPalette,[In] const D3DBOX* pSrcBox)</unmanaged>
        public static Result ToFile(Volume volume, string fileName, ImageFileFormat format, Box box, PaletteEntry[] palette)
        {
            unsafe
            {
                return D3DX9.SaveVolumeToFileW(fileName, format, volume, palette, new IntPtr(&box));
            }
        }

        /// <summary>
        /// Saves a volume to a <see cref="DataStream"/>.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileInMemory([In] ID3DXBuffer** ppDestBuf,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In, Buffer] const PALETTEENTRY* pSrcPalette,[In] const void* pSrcBox)</unmanaged>
        public static DataStream ToStream(Volume volume, ImageFileFormat format)
        {
            Blob blob;
            D3DX9.SaveVolumeToFileInMemory(out blob, format, volume, null, IntPtr.Zero);
            return new DataStream(blob);
        }

        /// <summary>
        /// Saves a volume to a <see cref="DataStream"/>.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="format">The format.</param>
        /// <param name="box">The box.</param>
        /// <returns>
        /// A <see cref="SharpDX.Result"/> object describing the result of the operation.
        /// </returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileInMemory([In] ID3DXBuffer** ppDestBuf,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In, Buffer] const PALETTEENTRY* pSrcPalette,[In] const void* pSrcBox)</unmanaged>
        public static DataStream ToStream(Volume volume, ImageFileFormat format, Box box)
        {
            return ToStream(volume, format, box, null);
        }

        /// <summary>
        /// Saves a volume to a <see cref="DataStream"/>. 
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <param name="format">The format.</param>
        /// <param name="box">The box.</param>
        /// <param name="palette">The palette.</param>
        /// <returns>A <see cref="SharpDX.Result" /> object describing the result of the operation.</returns>
        /// <unmanaged>HRESULT D3DXSaveVolumeToFileInMemory([In] ID3DXBuffer** ppDestBuf,[In] D3DXIMAGE_FILEFORMAT DestFormat,[In] IDirect3DVolume9* pSrcVolume,[In, Buffer] const PALETTEENTRY* pSrcPalette,[In] const void* pSrcBox)</unmanaged>
        public static DataStream ToStream(Volume volume, ImageFileFormat format, Box box, PaletteEntry[] palette)
        {
            unsafe
            {
                Blob blob;
                D3DX9.SaveVolumeToFileInMemory(out blob, format, volume, palette, new IntPtr(&box));
                return new DataStream(blob);
            }
        }
    }
}