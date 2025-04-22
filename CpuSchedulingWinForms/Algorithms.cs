using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] p = new double[np];
            double[] p2 = new double[np];
            double[] ap = new double[np];
            double[] ap2 = new double[np];
            Random rnd = new Random();

            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (rand)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        p[num] = rnd.Next(1, 26);
                        ap[num] = rnd.Next(0, 51);
                    }

                    for (num = 0; num <= np - 1; num++)
                    {
                        p2[num] = p[num];
                    }
                }
                else
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
                }
                

                int activetime = 0;
                int time = 0;
                int[] porder = new int[np];
                double[] cop = new double[np];

                for (num = 0; num < np; num++)
                {
                    porder[num] = num;
                }
                for (num = 0; num < np; num++)
                {
                    ap2[num] = ap[num];
                }

                for (num = 0; num < np - 1; num++)
                {
                    for (int i = 0; i < np - 1 - num; i++)
                    {
                        if (ap2[i] > ap2[i + 1])
                        {
                            double temp = ap2[i];
                            ap2[i] = ap2[i+1];
                            ap2[i+1] = temp;

                            /*temp = p[i];
                            p[i] = p[i+1];
                            p[i+1] = temp;*/

                            temp = porder[i];
                            porder[i] = porder[i+1];
                            porder[i + 1] = (int)temp;
                        }
                    }
                }



                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[porder[num]] = 0;
                        time += (int)p[porder[num]] + (int)ap[porder[num]];
                        cop[porder[num]] = time;
                        activetime += (int)p[porder[num]];
                    }
                    else
                    {
                        if (time < ap[porder[num]])
                        {
                            time += ((int)ap[porder[num]] - time);
                            time += (int)p[porder[num]];
                            cop[porder[num]] = time;
                            wtp[porder[num]] = 0;
                        }
                        else
                        {
                            wtp[porder[num]] = time - ap[porder[num]];
                            time += (int)p[porder[num]];
                            cop[porder[num]] = time;
                            activetime += (int)p[porder[num]];
                        }
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

        public static void sjfAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt16(userInput);
            Random rnd = new Random();
            double[] ap = new double[np];
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double[] cop = new double[np];

            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (rand)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        bp[num] = rnd.Next(1, 26);
                        ap[num] = rnd.Next(0, 51);
                    }
                }
                else
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        string input =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                        bp[num] = Convert.ToInt64(input);

                        string input2 =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter Arrival time: ",
                                                               "Arrival time for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                        ap[num] = Convert.ToInt64(input2);
                    }
                }
                double[] p2 = new double[np];
                double[] ap2 = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                    p2[num] = bp[num];
                    ap2[num] = ap[num];
                }
                for (x = 0; x < np - 1; x++)
                {
                    for (num = 0; num < np - 1 - x; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;

                            temp = ap[num];
                            ap[num] = ap[num + 1];
                            ap[num + 1] = temp;
                        }
                    }
                }

                int time = 0;
                int activetime = 0;
                int completed = 0;
                bool ready = false;
                while (completed < np)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        if (time < ap[num])
                        {
                            continue;
                        }
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && ap[num] == ap2[x] && found == false)
                            {
                                wtp[x] = time - ap[num];
                                time += (int)bp[x];
                                activetime += (int)bp[x];
                                cop[x] = time;
                                completed++;
                                bp[x] = 0;
                                found = true;
                                ready = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            found = false;
                            break;
                        }
                        found = false;
                    }

                    if (!ready)
                    {
                        time++;
                    }
                    else
                    {
                        ready = false;
                    }
                }

                

                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num+1) + " - Burst Time: " + p2[num] + " - Arrival Time: " + ap2[num] + "\n";

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
                    tap[num] = cop[num] - ap2[num];
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
        }

        public static void priorityAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                double[] ap = new double[np];
                Random rnd = new Random();
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;

                if (rand)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        bp[num] = rnd.Next(1, 26);
                        ap[num] = rnd.Next(0, 51);
                        p[num] = rnd.Next(1, 16);
                    }
                }
                else
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        string input =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                        bp[num] = Convert.ToInt64(input);

                        string input2 =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                               "Priority for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                        p[num] = Convert.ToInt16(input2);

                        string input3 =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter Arrival Time: ",
                                                               "Priority for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                        ap[num] = Convert.ToInt16(input3);
                    }
                }
                double[] bp2 = new double[np];
                double[] ap2 = new double[np];
                double[] p2 = new double[np];
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                    bp2[num] = bp[num];
                    ap2[num] = ap[num];
                    p2[num] = p[num];
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

                            temp = (int)bp2[num];
                            bp2[num] = bp2[num + 1];
                            bp2[num + 1] = temp;

                            temp = (int)ap2[num];
                            ap2[num] = ap2[num + 1];
                            ap2[num + 1] = temp;
                        }
                    }
                }

                int time = 0;
                int activetime = 0;
                double[] cop = new double[np];
                bool ready = false;
                int completed = 0;
                while (completed < np)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        if (time < ap2[num])
                        {
                            continue;
                        }

                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && ap2[num] == ap[x] && bp2[num] == bp[x] && found == false)
                            {
                                wtp[x] = time - ap2[num];
                                time += (int)bp2[num];
                                activetime += (int)bp2[num];
                                cop[x] = time;
                                p[x] = 0;
                                completed++;
                                found = true;
                                ready = true;
                            }
                        }
                        found = false;
                    }

                    if (!ready)
                    {
                        time++;
                    }
                    else
                    {
                        ready = false;
                    }
                }

                String summary = "";
                String pdata = "";

                for (num = 0; num <= np - 1; num++)
                {
                    pdata += "Process " + (num + 1) + " - Burst Time: " + bp[num] + " - Arrival Time: " + ap[num] + " - Priority: " + p2[num] + "\n";

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
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt32(userInput);
            int i, counter = 0;
            double total = 0.0;
            Random rnd = new Random();
            double timeQuantum;
            double[] wtp = new double[np];
            double[] tap = new double[np];
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            double[] temp = new double[np];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (rand)
                {
                    for (i = 0; i < np; i++)
                    {
                        burstTime[i] = rnd.Next(1, 26);
                        arrivalTime[i] = rnd.Next(0, 51);
                        temp[i] = burstTime[i];
                    }
                    timeQuantum = rnd.Next(1, 5);
                    Helper.QuantumTime = "" + timeQuantum;
                }
                else
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
                }

                double active = 0;
                for (total = 0, i = 0; x != 0;)
                {
                    if (arrivalTime[i] <= total)
                    {
                        if (temp[i] <= timeQuantum && temp[i] > 0)
                        {
                            total += temp[i];
                            active += temp[i];
                            temp[i] = 0;
                            counter = 1;
                        }
                        else if (temp[i] > 0)
                        {
                            temp[i] -= timeQuantum;
                            total += timeQuantum;
                            active += timeQuantum;
                        }


                        if (temp[i] == 0 && counter == 1)
                        {
                            x--;
                            //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                            //MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                            //MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                            turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                            tap[i] = total - arrivalTime[i];
                            waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);
                            wtp[i] = total - arrivalTime[i] - burstTime[i];
                            counter = 0;
                        }
                        if (i == np - 1)
                        {
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        if (i == np - 1)
                        {
                            i = 0;
                            total++;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    

                    
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                //MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                //MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);



                String summary = "";
                String pdata = "";

                for (i = 0; i <= np - 1; i++)
                {
                    pdata += "Process " + (i + 1) + " - Burst Time: " + burstTime[i] + " - Arrival Time: " + arrivalTime[i] + "\n";

                }
                pdata += "Time Quantum: " + timeQuantum;

                MessageBox.Show(pdata, "Process Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String waitdata = "";
                for (i = 0; i <= np - 1; i++)
                {
                    waitdata += "Process " + (i + 1) + " - Wait Time: " + wtp[i] + "\n";
                }

                waitdata += "Average Wait Time: " + averageWaitTime;
                summary += "Average Wait Time: " + averageWaitTime + " (secs)\n";

                MessageBox.Show(waitdata, "Wait Time Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                String turnarounddata = "";
                for (i = 0; i <= np - 1; i++)
                {
                    turnarounddata += "Process " + (i + 1) + " - Turnaround Time: " + tap[i] + "\n";
                }

                turnarounddata += "Average Turnaround Time: " + averageTurnaroundTime;
                summary += "Average Turnaround Time: " + averageTurnaroundTime + " (secs)\n";

                MessageBox.Show(turnarounddata, "Turnaround Data", MessageBoxButtons.OK, MessageBoxIcon.None);

                summary += "CPU Utilization: " + (active / total * 100) + "%\n";
                summary += "Throughput: " + ((double)np / (double)total) + " processes per second\n";


                MessageBox.Show(summary, "Summary", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }


        // New Algorithms Below

        public static void srtfAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt16(userInput);
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];
            Random rnd = new Random();
            int activetime = 0;
            double[] cop = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt;
            int num;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (rand)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        p[num] = rnd.Next(1, 26);
                        ap[num] = rnd.Next(0, 51);
                    }

                    for (num = 0; num <= np - 1; num++)
                    {
                        p2[num] = p[num];
                    }
                }
                else
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

        

        public static void hrrnAlgorithm(string userInput, bool rand)
        {
            int np = Convert.ToInt16(userInput);
            double[] ap = new double[np];
            double[] p = new double[np];
            double[] p2 = new double[np];
            double[] cop = new double[np];
            Random rnd = new Random();
            double[] wtp = new double[np];
            int activetime = 0;
            double twt = 0.0, awt;
            int num;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (rand)
                {
                    for (num = 0; num <= np - 1; num++)
                    {
                        p[num] = rnd.Next(1, 26);
                        ap[num] = rnd.Next(0, 51);
                    }

                    for (num = 0; num <= np - 1; num++)
                    {
                        p2[num] = p[num];
                    }
                }
                else
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

        
    }
}

