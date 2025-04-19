using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);                        
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);
            }
        }


        // New Algorithms Below

        public static void srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];

            int activetime = 0;
            double[] cop = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt;
            int num;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    p[num] = Convert.ToInt64(input);
                    p2[num] = Convert.ToInt64(input);


                    string input2 =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Arrival time: ",
                                                       "Arrival time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    ap[num] = Convert.ToInt64(input2);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }


                int completed = 0;
                int minindex = -1;
                int time = 0;

                while (completed < np)
                {
                    double minburst = double.MaxValue;
                    for (num = 0; num <= np - 1; num++)
                    {
                        if (p[num] < minburst && ap[num] <= time && p[num] != 0)
                        {
                            minindex = num;
                            minburst = p[num];
                        }
                    }

                    for (num = 0; num <= np - 1; num++)
                    {
                        if (ap[num] <= time && p[num] > 0 && num != minindex)
                        {
                            wtp[num]++;
                        }
                    }

                    if (minindex != -1)
                    {
                        p[minindex]--;
                        activetime++;
                        if (p[minindex] == 0)
                        {
                            completed++;
                            cop[minindex] = time;
                        }
                    }



                    time++;
                }



                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num + 1) + " - Burst Time: " + p2[num] + " - Arrival Time: " + ap[num] + "\n";
                }

                MessageBox.Show(pdata, "Process Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String waitdata = "";
                for (num = 0; num <= np - 1; num++)
                {
                    waitdata += "Process " + (num + 1) + " - Wait Time: " + wtp[num] + "\n";
                }


                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                waitdata += "Average Wait Time: " + awt;
                summary += "Average Wait Time: " + awt + " (secs)\n";

                MessageBox.Show(waitdata, "Wait Time Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String turnarounddata = "";
                double[] tap = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    tap[num] = cop[num] - ap[num];
                    turnarounddata += "Process " + (num + 1) + " - Turnaround Time: " + tap[num] + "\n";
                }

                double ttt = 0;
                for (num = 0; num <= np - 1; num++)
                {
                    ttt += tap[num];
                }
                ttt /= np;
                turnarounddata += "Average Turnaround Time: " + ttt;
                summary += "Average Turnaround Time: " + ttt + " (secs)\n";

                MessageBox.Show(turnarounddata, "Turnaround Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                summary += "CPU Utilization: " + ((double)activetime / (double)time * 100) + "%\n";
                summary += "Throughput: " + ((double)np / (double)time) + " processes per second\n";


                MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void srtfRandAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];
            double[] cop = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt;
            Random rnd = new Random();
            int num;
            int activetime = 0;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = rnd.Next(1, 11);
                    ap[num] = rnd.Next(0, 11);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    p2[num] = p[num];
                }

                int completed = 0;
                int minindex = -1;
                int time = 0;

                while (completed < np)
                {
                    double minburst = double.MaxValue;
                    for (num = 0; num <= np - 1; num++)
                    {
                        if (p[num] < minburst && ap[num] <= time && p[num] != 0)
                        {
                            minindex = num;
                            minburst = p[num];
                        }
                    }

                    for (num = 0; num <= np - 1; num++)
                    {
                        if (ap[num] <= time && p[num] > 0 && num != minindex)
                        {
                            wtp[num]++;
                        }
                    }

                    if (minindex != -1)
                    {
                        p[minindex]--;
                        activetime++;
                        if (p[minindex] == 0)
                        {
                            completed++;
                            cop[minindex] = time;
                        }
                    }



                    time++;
                }

                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num + 1) + " - Burst Time: " + p2[num] + " - Arrival Time: " + ap[num] + "\n";
                }

                MessageBox.Show(pdata, "Process Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String waitdata = "";
                for (num = 0; num <= np - 1; num++)
                {
                    waitdata += "Process " + (num + 1) + " - Wait Time: " + wtp[num] + "\n";
                }


                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                waitdata += "Average Wait Time: " + awt;
                summary += "Average Wait Time: " + awt + " (secs)\n";

                MessageBox.Show(waitdata, "Wait Time Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String turnarounddata = "";
                double[] tap = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    tap[num] = cop[num] - ap[num];
                    turnarounddata += "Process " + (num + 1) + " - Turnaround Time: " + tap[num] + "\n";
                }

                double ttt = 0;
                for (num = 0; num <= np - 1; num++)
                {
                    ttt += tap[num];
                }
                ttt /= np;
                turnarounddata += "Average Turnaround Time: " + ttt;
                summary += "Average Turnaround Time: " + ttt + " (secs)\n";

                MessageBox.Show(turnarounddata, "Turnaround Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                summary += "CPU Utilization: " + ((double)activetime / (double)time * 100) + "%\n";
                summary += "Throughput: " + ((double)np / (double)time) + " processes per second\n";


                MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];
            double[] cop = new double[np];

            double[] wtp = new double[np];
            int activetime = 0;
            double twt = 0.0, awt;
            int num;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    p[num] = Convert.ToInt64(input);
                    p2[num] = Convert.ToInt64(input);


                    string input2 =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Arrival time: ",
                                                       "Arrival time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    ap[num] = Convert.ToInt64(input2);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }


                int completed = 0;
                int maxindex = -1;
                int time = 0;


                while (completed < np)
                {
                    double maxratio = double.MinValue;
                    double rr = 0.0;
                    if (maxindex == -1)
                    {
                        for (num = 0; num <= np - 1; num++)
                        {
                            rr = ((time - ap[num]) + p[num]) / (p[num]);
                            if (rr > maxratio && ap[num] <= time && p[num] != 0)
                            {
                                maxratio = rr;
                                maxindex = num;
                            }
                            else if (rr == maxratio && ap[num] <= time && p[num] != 0)
                            {
                                if (p[num] < p[maxindex])
                                {
                                    maxindex = num;
                                }
                            }
                        }
                    }

                    if (maxindex != -1)
                    {
                        wtp[maxindex] = time - ap[maxindex];
                        time += (int)p[maxindex];
                        cop[maxindex] = time;
                        activetime += (int)p[maxindex];
                        p[maxindex] = 0;
                        completed++;
                        maxindex = -1;
                    }
                    else
                    {
                        time++;
                    }
                }

                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num + 1) + " - Burst Time: " + p2[num] + " - Arrival Time: " + ap[num] + "\n";
                }

                MessageBox.Show(pdata, "Process Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String waitdata = "";
                for (num = 0; num <= np - 1; num++)
                {
                    waitdata += "Process " + (num + 1) + " - Wait Time: " + wtp[num] + "\n";
                }


                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                waitdata += "Average Wait Time: " + awt;
                summary += "Average Wait Time: " + awt + " (secs)\n";

                MessageBox.Show(waitdata, "Wait Time Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String turnarounddata = "";
                double[] tap = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    tap[num] = cop[num] - ap[num];
                    turnarounddata += "Process " + (num + 1) + " - Turnaround Time: " + tap[num] + "\n";
                }

                double ttt = 0;
                for (num = 0; num <= np - 1; num++)
                {
                    ttt += tap[num];
                }
                ttt /= np;
                turnarounddata += "Average Turnaround Time: " + ttt;
                summary += "Average Turnaround Time: " + ttt + " (secs)\n";

                MessageBox.Show(turnarounddata, "Turnaround Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                summary += "CPU Utilization: " + ((double)activetime / (double)time * 100) + "%\n";
                summary += "Throughput: " + ((double)np / (double)time) + " processes per second\n";


                MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.None);






                /*for (num = 0; num <= np - 1; num++)
                {
                    MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);*/
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void hrrnRandAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            Random rnd = new Random();
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];
            double[] cop = new double[np];
            int activetime = 0;

            double[] wtp = new double[np];
            double twt = 0.0, awt;
            int num;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = rnd.Next(1, 11);
                    ap[num] = rnd.Next(0, 11);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    p2[num] = p[num];
                }


                int completed = 0;
                int maxindex = -1;
                int time = 0;


                while (completed < np)
                {
                    double maxratio = double.MinValue;
                    double rr = 0.0;
                    if (maxindex == -1)
                    {
                        for (num = 0; num <= np - 1; num++)
                        {
                            rr = ((time - ap[num]) + p[num]) / (p[num]);
                            if (rr > maxratio && ap[num] <= time && p[num] != 0)
                            {
                                maxratio = rr;
                                maxindex = num;
                            }
                            else if (rr == maxratio && ap[num] <= time && p[num] != 0)
                            {
                                if (p[num] < p[maxindex])
                                {
                                    maxindex = num;
                                }
                            }
                        }
                    }

                    if (maxindex != -1)
                    {
                        wtp[maxindex] = time - ap[maxindex];
                        time += (int)p[maxindex];
                        cop[maxindex] = time;
                        activetime += (int)p[maxindex];
                        p[maxindex] = 0;
                        completed++;
                        maxindex = -1;
                    }
                    else
                    {
                        time++;
                    }
                }



                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num + 1) + " - Burst Time: " + p2[num] + " - Arrival Time: " + ap[num] + "\n";
                }

                MessageBox.Show(pdata, "Process Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String waitdata = "";
                for (num = 0; num <= np - 1; num++)
                {
                    waitdata += "Process " + (num + 1) + " - Wait Time: " + wtp[num] + "\n";
                }


                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                waitdata += "Average Wait Time: " + awt;
                summary += "Average Wait Time: " + awt + " (secs)\n";

                MessageBox.Show(waitdata, "Wait Time Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String turnarounddata = "";
                double[] tap = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    tap[num] = cop[num] - ap[num];
                    turnarounddata += "Process " + (num + 1) + " - Turnaround Time: " + tap[num] + "\n";
                }

                double ttt = 0;
                for (num = 0; num <= np - 1; num++)
                {
                    ttt += tap[num];
                }
                ttt /= np;
                turnarounddata += "Average Turnaround Time: " + ttt;
                summary += "Average Turnaround Time: " + ttt + " (secs)\n";

                MessageBox.Show(turnarounddata, "Turnaround Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                summary += "CPU Utilization: " + ((double)activetime / (double)time * 100) + "%\n";
                summary += "Throughput: " + ((double)np / (double)time) + " processes per second\n";


                MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }
    }
}

