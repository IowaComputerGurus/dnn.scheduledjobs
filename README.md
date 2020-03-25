# ICG DNN Scheduled SQL Jobs
[![Build status](https://iowacomputergurus.visualstudio.com/_apis/public/build/definitions/50c931be-dbcb-44dd-a9bc-c5ae281e4051/22/badge)](https://iowacomputergurus.visualstudio.com/_apis/public/build/definitions/50c931be-dbcb-44dd-a9bc-c5ae281e4051/22/badge)

Keeping a DotNetNuke site database clean can be a real nightmare for those hosting sites on shared hosting providers.  With this free module, we can help reduce the effort needed to keep your database working smooth as can be.

This host only module is great for running scheduled tasks toclean the EventLog, Site Log, Shrink the Database and other common tasks.  The module comes pre-configured with a number of jobs that will help improve the performance of your database.  It also supports the ability to define custom job types to execute tasks of your own.

With full support for regular intervals of execution it makes database administration easy.

## Minimum DNN Version

Starting with Version 08.00.04 of this module you must be using DNN Version 9.0.0 or later.  

## Included Tasks

The following tasks are included as part of the default installation of this module.

* Clear Admin Logs - This process clears the internal Exceptions, ExceptionEvents, and EventLog tables within DNN Platform.  These tables can at times grow excessivly and this job does a proper truncate by removing the Foreign Keys, Truncating Data, and Re-adding the Foreign Keys.  
* Clean Site Log - This purges the 'SiteLog' database table.  This table should not be used in current versions of DNN
* Shrink Database - This executes a 'DBCC SHRINKDATABASE' command for the current database
