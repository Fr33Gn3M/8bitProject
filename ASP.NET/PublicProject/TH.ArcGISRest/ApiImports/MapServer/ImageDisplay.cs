using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

[Serializable()]
public class ImageDisplay
{
	public int Height { get; set; }
	public int DPI { get; set; }
	public int Width { get; set; }
}
