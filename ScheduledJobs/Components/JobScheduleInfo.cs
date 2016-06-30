/*
 * Copyright (c) 2008-2010 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
 * Copyright Contact: webmaster@iowacomputergurus.com
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to use, 
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software. 
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
 * */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    /// <summary>
    /// This class holds information on a specific job schedule item.
    /// </summary>
    [DebuggerDisplay("JobScheduleId: {JobScheduleId}, JobTitle: {JobTitle}")]
    public class JobScheduleInfo
    {
        public int JobScheduleId { get; set; }
        public int JobTypeId { get; set; }
        public string JobTitle { get; set; }
        public string JobScript { get; set; }
        public int JobFrequencyValue { get; set; }
        public string JobFrequencyType { get; set; }
        public DateTime NextJobRun { get; set; }
        public DateTime LastJobRun { get; set; }

        public string Schedule
        {
            get { return JobFrequencyValue.ToString() + " " + JobFrequencyType; }
        }
    }
}
