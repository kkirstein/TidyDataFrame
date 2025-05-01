# TidyDataFrame

_These libraries are in a very early state. Please expect bugs, incomplete features, and a changing API_

A wrapper and extension(s) around `Microsoft.Data.Analysis` package to support tidy data handling, like in the `tidyverse` packages for `R`.
See also [tidyverse](https://www.tidyverse.org/) and [tidyr package](https://tidyr.tidyverse.org/).

## Definition of Tidy Data

Tidy data is data where:

1. Each variable is a column; each column is a variable.
2. Each observation is a row; each row is an observation.
3. Each value is a cell; each cell is a single value.

Tidy data describes a standard way of storing data.

## Grammar of Graphics

_Grammar of Graphics_ is an attempt to unify the descriptipon of plots and charts for visualization of data sources. It is the base of the well-known `ggplot2` package for `R` (see [ggplot2](https://ggplot2.tidyverse.org/))
One goal of the `TidyDataFrame` ecosystem is to provide a plotting API for `C#`, based on _ Grammar of Graphics_, like `seaborn` provides for `Python`.

## Microsoft.Data.Analysis

This library builds upon the data frame implementation of `Microsoft.Data.Analysis`, whose documentation can be found [here](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.analysis?view=ml-dotnet-preview).

## Components

The _TidyDataFrame_ ecosystem currently consist of the following components:

* `TidyDataFrame`: Basic library to provide tidy data handling methods for `Microsoft.Data.Analysis`
* `TidyDataFrame.Extensions`: Extension methods for tidy data handling of `Microsoft.Data.Analysis.DataFrame`
* `TidyDataFrame.Examples`: Some example data sets to demonstrate and test `TidyDataFrame`
* `TidyDataFrame.Plot`: Provide a plotting API according to _Grammar of Graphics_ scheme

