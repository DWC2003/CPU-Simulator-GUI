** READ ME FOR CPU SCHEDULING SIMULATOR **

This project is a fork of a project by Francis Nweke which simulates different CPU scheduling algorithms.

This fork implements two new algorithms: Shortest Remaining Time First and Highest Response Ratio Next. These new algorithms have their own buttons as well on the GUI. It also implements a "Random Data" checkbox to create processes with random numbers instead of manually entering them one by one.

A good number of modifications were also made to the existing code. All algorithms now track two new statistics about the algorithm: CPU utilization and throughput. All stats are now additionally displayed in bulk rather than in a one-by-one series of pop-ups for easy viewing.

This project can be run by navigating to the CpuSchedulingWinForms/bin/release/ directory and selecting the .exe file. This is a windows form app, so the easiest way to build it yourself is to open the solution file (.sln) in visual studio and click the build button.
