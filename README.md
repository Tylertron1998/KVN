![Everyone hates KVN](https://vignette.wikia.nocookie.net/p__/images/5/5e/He%27s_back.png/revision/latest/scale-to-width-down/350?cb=20191004014622&path-prefix=protagonist)

KVN (Key Value Notation) is a JSON backed, simple to use, key value storage solution for .NET.

```cs
var settings = new DatabaseSettings("./path/to/file.json");
var instance = new DatabaseInstance<MyPoco>(settings);

long id = 1;

var poco = new MyPoco { Value = "Hello, .NET!" };

instance[id] = poco;

// call .SaveChanges to commit your changes.

instance.SaveChanges();

Console.WriteLine(instance[id]); // Hello, .NET!
```

KVN aims to be thread safe, fast, and easy to use.
