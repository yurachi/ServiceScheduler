The .NET classes for easy recurring scheduling.
The scheduler configuration is re-read by the background thread every 2 minutes.
The scheduler sleeps for a default minimal time or until the next execution time, whichever comes sooner.

The datasource might contain entries 
HH:MM [weekday] [once]
now | once
weekday stop [once]

Examples:
0:10 Monday
18:51
11:15 once
now
once
0:0
Sunday stop once
 

"now" or "once" alone is going to be replaced by concrete execution time and removed from database.
Entries ending with "once" are going to be removed on execution. 
The Year is assumed current / next. The day is assumed current / next.
"stop" means to sleep on that weekday. The execution times for the "stop" day are removed from the list and the configurator sleeps up to the 23:59

The configurator allows for a small tolerance in missing the exact execution time. All the execution is happening in the background threads, so the consumers of the timer are expected to be thread-safe.

