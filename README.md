# Bulgarian stemmer in .NET Standard

## Stemmer
Bulgarian stemmer implementation of [BULSTEM](http://lml.bas.bg/~nakov/bulstem/) of Preslav Nakov.

### Sample usage
```csharp
BulgarianStemmer stemmer = new BulgarianStemmer();
stemmer.LoadRules();

string stemmed = stemmer.Stem('уникален');
// Do something with stemmed word...
```

## Analyzer
This project also includes an analyzer for Lucene.NET version 4.8.0 implementing the stemmer, which is not finished. It needs some work to be functional.

## License
This project is licensed under the terms of the MIT license.
