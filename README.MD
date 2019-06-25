## 1. The Idea and description
The goal of this procjet is facilitate asp.net backend localization of your sites. It helps to translate texts to different languages by predefined dictionaries.

It was inspired by [@ngx-translate](https://github.com/ngx-translate/core). 
The idea is simple: you create dictionaries in form of json files where you store values which will replace translation keys.

## 2. Installation 

Insatll this package via NuGet
```
Install-Package Ruteco.AspNetCore.Translate
```

## 3. Configuration
#### Create dictionaries
Create .json files with name of a language you will use and place them into one directory, like: 
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

    var message =  translationService.Get("en","Sports.Football");
```

##### ITranslationService properties
| Name        | Type           | Description  |
| ------------- |:-------------:| -----:|
| Get()    | (string, string) => string) | Get translation for particular *language* for particular *key* (**key** is string in form of dot-separated properties hierarchy which applied to json objects). If there is no such language or key inside a dictionary, **key** itself will be returned value  |
| IsInitialized      | bool      |   Tells whether dictionaries been loaded |
| DictionariesLocation | string      |  Path which was used for initialization of dictionaries |
| Languages | IEnumerable< string >      |   List of languages (it been took from .json dictionaries files names) |


## 5. Notes

This was quick implementation, probably not in very elegant way which uses static fields in support class.
if you want to reinitialize vocabularies or do not want to use Dependancy injection create *ITranslationServiceBuilder* instance and call Init(path) method.
```C#
    IServiceCollection services = new ServiceCollection();
    var builder = new TranslationServiceBuilder(services);
    builder.Init(dictionariesLocation);

    var translationService = new TranslationService();
```

Feel free to contact me or create pull requests :) 