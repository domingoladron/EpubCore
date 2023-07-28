# EpubCore\.Cli Verbs

## add\-resource

Add a new resource from the EPub\.

```
add-resource <parameters>
```

### Parameters

####  \-\-add\-resource \( \-a \)

Path to new new EPub resource to add

####  \-\-before\-resource \( \-b \)

Add before this existing EPub resource \(name\-of\-existing\.html\)

####  \-\-resource\-type \( \-t \)

Type of EPub resource \(Html, Css, Font, Image, Other\)

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## extract

Extract the contents of this EPub file

```
extract <parameters>
```

### Parameters

####  \-\-destination \( \-d \)

path to destination directory of where to extract the EPub's files

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## gen\-cli\-docs

Generate documentation for the CLI

```
gen-cli-docs <parameters>
```

### Parameters

####  \-\-out \( \-o \)

File path to write documentation for the CLI

## get\-details

Get details about this EPub file

```
get-details <parameters>
```

### Parameters

####  \-\-filter \( \-f \)

Pipe\-delimited filter keys of the data you want to be returned\.  Filter values include: Uniqueidentifier\|Version\|Authors\|Publishers\|Contributors\|Titles\|Toc\|Css\|Cover\|Images\|Fonts\|Html

####  \-\-format \( \-f \)

Output format of data \(Json or Yaml\)

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## get\-html

Get contents of html file

```
get-html <parameters>
```

### Parameters

####  \-\-html\-file \( \-h \)

The nme of the html file for which to fetch the contents

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## remove\-resource

Remove an existing resource from the EPub\.

```
remove-resource <parameters>
```

### Parameters

####  \-\-existing\-resource \( \-e \)

Name of existing EPub resource \(name\-of\-existing\.html\)

####  \-\-resource\-type \( \-t \)

Type of EPub resource \(Html, Css, Font, Image, Other\)

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## replace\-cover

Replace the cover image in the epub\.

```
replace-cover <parameters>
```

### Parameters

####  \-\-cover\-img \( \-c \)

Path to new cover image \(\.jpg, \.png\)

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## replace\-html\-content

Replace the contents of an existing html with new html content\.

```
replace-html-content <parameters>
```

### Parameters

####  \-\-html\-path \( \-h \)

Path to new html file \(path/to/new\.html\)

####  \-\-existing\-html \( \-e \)

Name of existing EPub html \(name\-of\-existing\.html\)

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## replace\-css\-content

Replace the contents of an existing css with new css\.

```
replace-css-content <parameters>
```

### Parameters

####  \-\-css\-path \( \-c \)

Path to new stylesheet file \(path/to/new\.css\)

####  \-\-existing\-css \( \-e \)

Name of existing EPub stylesheet \(name\-of\-existing\.css\)

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## update\-authors

Update the author\(s\) in the epub\.

```
update-authors <parameters>
```

### Parameters

####  \-\-author \( \-a \)

Author to add to your EPub

####  \-\-clear\-previous \( \-c \)

Clear previous authors

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## update\-publisher

Update the publisher\(s\) in the epub\.

```
update-publisher <parameters>
```

### Parameters

####  \-\-publisher \( \-p \)

Publisher to add to your EPub

####  \-\-clear\-previous \( \-c \)

Clear previous publisher

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging

## update\-titles

Update the title\(s\) in the epub\.

```
update-titles <parameters>
```

### Parameters

####  \-\-titles \( \-t \)

Titles to swap out of our EPub

####  \-\-out \( \-o \)

Path to write final epub\.  If empty, overwrites input epub

####  \-\-in \( \-i \)

Path to epub to parse

####  \-\-verbose \( \-v \)

If true, writes heaps of logging