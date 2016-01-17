#gems-logger [![Build Status](https://travis-ci.org/thinkingmedia/gems-logger.svg?branch=master)](https://travis-ci.org/thinkingmedia/gems-logger) [![Code Climate](https://codeclimate.com/github/thinkingmedia/gems-logger/badges/gpa.svg)](https://codeclimate.com/github/thinkingmedia/gems-logger)  [![Test Coverage](https://codeclimate.com/github/thinkingmedia/gems-logger/badges/coverage.svg)](https://codeclimate.com/github/thinkingmedia/gems-logger/coverage)

A multithread friendly logger for C# that uses stack-able writers and a UI component 
for watching the output. Easy to use, reliable and used by me everyday.

##Getting Started

There is no configuring the logger class. The logger starts working as soon as you've
attached writers to the Logger.

You don't have to log to a file. Instead you the Logger UI component to display the log output
on the screen as part of a form.

Most writers in this project are designed to be stacked together.

To output to a file use the `FileWriter`.

	Logger.Add(new FileWriter(@"c:\application.log"));

To prefix each line in the log with a date/time stamp, and the class name that logged the entry.
Stack the `FileWriter` inside a `FormatWriter` like this.

	Logger.Add(new FormatWriter(new FileWriter(@"c:\application.log"), new DetailFormat()));

To log output from a specific worker thread to it's own file.

	Logger.Add(new ThreadWriter(new FileWriter(@"c:\application.log"), Thread.CurrentThread.ManagedThreadId));

To log error messages to a different file.

	Logger.Add(new LevelWriter(new FileWriter(@"c:\errors.log"), new[]{ Logger.eLEVEL.ERROR }));

###Creating A Logger

The logger is designed to be used on a *per class* bases. Each entry in the log
file will be identified by the class that published the log entry.

	public class MyExample
	{
		private static Logger _logger = Logger.Create(typeof(MyExample));

		public MyExample()
		{
			_logger.Fine("Hello World");
		}
	}

###Capturing Unhandled Exceptions

You can record unhandled exceptions by adding this line to your `Application` start up. Where `_logger_`
is a log handler like the one shown in the above example.

	Application.ThreadException += (pSender, pEventArgs) => _logger.Exception(pEventArgs.Exception);

###Close Before Existing

To close the output writers for the logger. Call the static `Close()` method on the
Logger class. This can easily be attached to the `ApplicationExit` event for 
Windows Applications.

		Application.ApplicationExit += (pSender, pArgs)=>Logger.Close();

##Windows Event Logger

You can record entries into the Windows Event log easily using the Logger's `EventLog` static
method.

It's a good idea to record unhandled exceptions during the Application's `Main()` start up
method to the Event logger. That can be done like this.

	internal static class Program
	{
		private static readonly Logger _logger = Logger.Create(typeof(Program));

		[STAThread]
		private static void Main()
		{
			try
			{
				Application.ThreadException += (pSender, pEventArgs) => _logger.Exception(pEventArgs.Exception);
				Application.ApplicationExit += (pSender, pArgs) => Logger.Close();
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				Logger.EventLog(ex);
			}
		}
	}
