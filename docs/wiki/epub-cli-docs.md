# EpubCore\.Cli Verbs

## add\-resource

Add a new resource from the EPub\.

```
epub add-resource <parameters>
```

### Parameters

####  \-\-add\-resource \( \-a \) \[required\]

Path to new new EPub resource to add

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-resource\-type \( \-t \) \[required\]

Type of EPub resource \(Html, Css, Font, Image, Other\)

####  \-\-before\-resource \( \-b \) 

Add before this existing EPub resource \(name\-of\-existing\.html\)

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## extract

Extract the contents of this EPub file

```
epub extract <parameters>
```

### Parameters

####  \-\-destination \( \-d \) \[required\]

path to destination directory of where to extract the EPub's files

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## gen\-cli\-docs

Generate documentation for the CLI

```
epub gen-cli-docs <parameters>
```

### Parameters

####  \-\-out \( \-o \) \[required\]

File path to write documentation for the CLI

## get\-details

Get details about this EPub file

```
epub get-details <parameters>
```

### Parameters

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-filter \( \-f \) 

Pipe\-delimited filter keys of the data you want to be returned\.  Filter values include: Uniqueidentifier\|Version\|Authors\|Publishers\|Contributors\|Titles\|Toc\|Css\|Cover\|Images\|Fonts\|Html

####  \-\-format \( \-f \) 

Output format of data \(Json or Yaml\)

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## get\-html

Get contents of html file

```
epub get-html <parameters>
```

### Parameters

####  \-\-html\-file \( \-h \) \[required\]

The nme of the html file for which to fetch the contents

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## remove\-resource

Remove an existing resource from the EPub\.

```
epub remove-resource <parameters>
```

### Parameters

####  \-\-existing\-resource \( \-e \) \[required\]

Name of existing EPub resource \(name\-of\-existing\.html\)

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-resource\-type \( \-t \) \[required\]

Type of EPub resource \(Html, Css, Font, Image, Other\)

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## replace\-cover

Replace the cover image in the epub\.

```
epub replace-cover <parameters>
```

### Parameters

####  \-\-cover\-img \( \-c \) \[required\]

Path to new cover image \(\.jpg, \.png\)

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## replace\-css\-content

Replace the contents of an existing css with new css\.

```
epub replace-css-content <parameters>
```

### Parameters

####  \-\-css\-path \( \-c \) \[required\]

Path to new stylesheet file \(path/to/new\.css\)

####  \-\-existing\-css \( \-e \) \[required\]

Name of existing EPub stylesheet \(name\-of\-existing\.css\)

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## replace\-html\-content

Replace the contents of an existing html with new html content\.

```
epub replace-html-content <parameters>
```

### Parameters

####  \-\-existing\-html \( \-e \) \[required\]

Name of existing EPub html \(name\-of\-existing\.html\)

####  \-\-html\-path \( \-h \) \[required\]

Path to new html file \(path/to/new\.html\)

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## update\-authors

Update the author\(s\) in the epub\.

```
epub update-authors <parameters>
```

### Parameters

####  \-\-author \( \-a \) \[required\]

Author to add to your EPub

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-clear\-previous \( \-c \) 

Clear previous authors

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## update\-publisher

Update the publisher\(s\) in the epub\.

```
epub update-publisher <parameters>
```

### Parameters

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-publisher \( \-p \) \[required\]

Publisher to add to your EPub

####  \-\-clear\-previous \( \-c \) 

Clear previous publisher

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging

## update\-titles

Update the title\(s\) in the epub\.

```
epub update-titles <parameters>
```

### Parameters

####  \-\-in \( \-i \) \[required\]

Path to epub to parse

####  \-\-titles \( \-t \) \[required\]

Titles to swap out of our EPub

####  \-\-out \( \-o \) 

Path to write final epub\.  If empty, overwrites input epub

####  \-\-verbose \( \-v \) 

If true, writes heaps of logging