Here will be documentation and project description

## 1. Idea and description
This library was inspired by [@ngx-translate](https://github.com/ngx-translate/core). 
The idea is simple dictionaries in form of json files where you store values which will replace translation keys.

## 2. Installation 

Insatll this package via NuGet
```
Install-Package Ruteco.AspNetCore.Translate
```

## 3. Configuration
#### Create dictionaries
Create .json files with name of language you will use and place them into one directory, like: 
```
..\i18n\
        en.json
        en-gb.json
        ru.json
```
**Important note:** be aware that asp.net sites as CurrentDirectory use directory of the project (not /bin folder) so if you use class library or application, mark the files *Copy to output directory: Copy always* in Properties window of Solution explorer.


#### Add translations services in Startup.cs file for web application:
```C#
    var dictionariesLocation = System.IO.Directory.GetCurrentDirectory() + Configuration["TranslationsDictionariesLocation"];
    services.AddTranslations(dictionariesLocation);
```

#### For console aplication:
```C#
    IServiceCollection services = new ServiceCollection();
    var path = System.IO.Directory.GetParent(Assembly.GetAssembly(typeof(TranslateionServiecTests)).Location) + "\\i18n";
    services.AddTranslations(path);
```

## 4. Usage
Dependency injection will provide *ITranslationService* implementation into your controllers 
```C#
    public HomeController(ITranslationService translation)
    {
        _translationService = translation;
    }

    ...

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(_translationService.Get("en","Sports.Football"));
    }
```

or you can get it from *IServiceProvider*

```C#
        IServiceCollection services = new ServiceCollection();
        var path = System.IO.Directory.GetParent(Assembly.GetAssembly(typeof(TranslateionServiecTests)).Location) + "\\i18n";
        services.AddTranslations(path);
        provider = services.BuildServiceProvider();
        translationService = provider.GetService<ITranslationService>();

```


## 5. Notes