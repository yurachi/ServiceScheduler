The .NET classes for easy recurring scheduling.
The scheduler configuration is re-read by the background thread every 2 minutes.
The scheduler sleeps for a default minimal time or until the next execution time, whichever comes sooner.

The datasource might contain entries 
HH:MM[:SS] [DD-mmm]|[weekday]|[once]
now [once]

Examples:
0:10 Monday
08:01:01
11:15 Once
Now
0:00:01 01-Jan

"Now" is going to be replaced by concrete execution time and stored back in database, unless marked with "Once".
Entries ending with "Once" are going to be removed on execution. 
The Year is assumed current / next. The day is assumed current / next.

The configurator allows for a small tolerance in missing the exact execution time. All the execution is happening in the background threads, so the consumers of the timer are expected to be thread-safe.

