# The Klout Service no longer exists
Klout was shut down by its owners Lithium on the 25th of May 2018.  
Therefore this library no longer serves any purpose, and is left here for reference only.  

For full details on Klout being shutdown, see https://community.lithium.com/t5/Lithium-s-View-Blog/Sunsetting-Klout/ba-p/473363

# MonoKlout
[![Build status](https://ci.appveyor.com/api/projects/status/9myt51i2jmql8a0l?svg=true)](https://ci.appveyor.com/project/JoshKeegan/monoklout)  
  
MonoKlout is a library for Version 2 of the Klout API. It is built using the Mono framework and JSON.NET, and is especially aimed at use in mobile applications, specifically using MonoTouch or Mono for Android. 

# Features
* Support for all Version 2 calls
	* Identity
	* Score
		* Daily, Weekly, and Monthly changes
	* Influencers
	* Influencees
	* User Topics
* Works with the Mono framework and specifically at MonoTouch and Mono for Android (see [Xamarin](http://xamarin.com/))

# Getting Started
To get started, just instantiate the KloutAPI class.
```csharp
KloutApi klout = new KloutApi("apikey");
```
You can now access methods via the klout object (instance of KloutApi):
```csharp
KloutIdentityResponse identity = klout.GetKloutIdentity("twitterUsername");
KloutScoreResponse score = klout.GetKloutScore("kloutId");
KloutInfluenceResponse influence = klout.GetInfluence("kloutId");
List<KloutUserTopicsResponse> userTopics = klout.GetUserTopics("kloutId");

// Klout Id
string id = identity.Id;

// Klout Score
double scoreNumber = score.Score;
double dailyChange = score.ScoreDelta.DayChange;
double weeklyChange = score.ScoreDelta.WeekChange;
double monthlyChange = score.ScoreDelta.MonthChange;

// Klout Influence
KloutInfluenceEntityResponse[] influencees = influence.MyInfluencees;
KloutInfluenceEntityResponse[] influencers = influence.MyInfluencers;

foreach (KloutInfluenceEntityResponse influencee in influencees)
{
	// Information about influencee
	Console.WriteLine(influencee.Entity.Payload.KloutId);
	Console.WriteLine(influencee.Entity.Payload.Nick);
	Console.WriteLine(influencee.Entity.Payload.Score);
}

foreach (KloutInfluenceEntityResponse influencer in influencers)
{
	// Information about influencee
	Console.WriteLine(influencer.Entity.Payload.KloutId);
	Console.WriteLine(influencer.Entity.Payload.Nick);
	Console.WriteLine(influencer.Entity.Payload.Score);
}

// Klout User Topics
foreach (KloutUserTopicsResponse topic in userTopics)
{   
	// Information about the topic
	Console.WriteLine(topic.DisplayName);
	Console.WriteLine(topic.Name);
	Console.WriteLine(topic.Slug);
	Console.WriteLine(topic.ImageUrl);
}
```

#Exceptions
In order to make it as simple as possible to get started using MonoKlout (and prevent all calls to the library from having to be wrapped in a try/catch block), exceptions generated from contacting the Klout API do not get thrown.
Instead, if there was a problem fetching data from Klout, the library will return the default value for the return type (null for objects), so you should always check if the response is null before using it.  
  
If you want to access the exceptions, you can do so with:
```csharp
IEnumerable<WebException> exceptions = ExceptionHandler.GetExceptions()
```

This could then be used to log exceptions in your application like:
```csharp
public static void LogKloutExceptions(bool clearExceptions = true)
{
    WebException[] exceptions = ExceptionHandler.GetExceptions().ToArray();

    if (exceptions.Length > 0)
    {
        DefaultLog.Warn("Received {0} exceptions from Klout", exceptions.Length);

        //Log the individual exceptions
        foreach (WebException e in exceptions)
        {
            DefaultLog.Warn("Klout Exception:\n{0}", e);
        }

        //Clear the exceptions if requested
        if (clearExceptions)
        {
            ExceptionHandler.ClearExceptions();
        }
    }
}
```

You can also access just the last exception with:
```csharp
ExceptionHandler.GetLastException()
```

Which could be used to re-throw the exception to be handled at a higher level in your application:
```csharp
WebException e = ExceptionHandler.GetLastException();
if (e != null)
{
    throw e;
}
```

Note that there is a different ExceptionHandler instance per-thread, so if you have a multi-threaded application, each thread will only be able to see its own exceptions.  
This also means that if you were to make calls asyncronously with a Task, you'll need to check for exceptions from within the worked Task, as any exceptions will not be visible to the main thread.

# Issues
Please request any changes/features, or report any issues on the [Bug Tracker](https://github.com/JoshKeegan/MonoKlout/issues).

# Author
I (Pierce Boggan) wrote MonoKlout one weekend as a little side project. I'm currently a sophomore studying Software Engineering at Auburn University. You can visit my blog at pierceboggan.com.  
Development & maintenance of MonoKlout was taken over in 2015 by [Josh Keegan](https://github.com/JoshKeegan).
