# Dynamo API (v1)

This is a pubic API to Dynamo Software's Dynamo application (http://www.dynamosoftware.com/).

In order to use the API, you need a Dynamo account which has granted API access. Please, your administrator or Dynamo Software support for instructions how to acquire one.

This project features code samples in several technologies:
   * JavaScript - for web pages and server-side scripting.
   * C# (.Net Framework) - for desktop tools, backend processing etc.  

# How to use it

```code
git clone in a local folder
cd DynamoAPI
npm install jasmine
npm install content-disposition
jasmine --filter=test1 --stop-on-failure=true
```

# Endpoint URIs

All URI's are relative to [Dynamo URL]/new/v1, unless otherwise noted.

Example: https://d5.dynamosoftware.com/new/v1/version

# Change log

## [1.0.0] 2016-04-27
### Added
- Login method.
- Save method.
- Delete method.
- GetById method.
- GetByTemplate method.
- JavaScript samples.
- Documentation

## [1.0.1] 2016-05-20    
### Added
- GetDocument method.
- ExecuteCommand method.
### Changed
- JavaScript samples.

## [1.1.0] 2016-10-17    
### Added
- GetDocuments method.
### Changed
- JavaScript samples.

## [1.1.1] 2017-04-20    
### Added
- SearchDocuments method.
- C# samples added.
### Changed
- JavaScript samples.

## [1.2.0] 2017-06-26    
### Added
- Delete method.
- GetView method.
- GetViewSQL method.
- UploadFiles method.
### Changed
- GetByTemplate method modified to support paging of results.
- JavaScript samples.
- C# samples added.

# Description

This is a high-level overview of the provided API. For detail description of the format of the input and output arguments, please check the specific sample code provided with this project. 

# API Reference

Login
-----
Performs a login into existing tenant. If the login is successful, will return a session token. This session token will also be assigned as a HTTP-only cookie.

   * Method: POST
   * URL: /login
   * Query string: userName=<code>username</code>&password=<code>password</code>&tenant=<code>tenant</code>
   * Response: application/json
   
   ```json
   { "sidt": "sessionToken" }
   ```

Save
-----
Creates or updates an item. The item is identified by the entity schema name (<code>es</code>) and item's identifier (<code>id</code>). The properties to be set are provided as a key/value pair.

   * Method: POST
   * URL: /save
   * Body: Stringified JSON object that must contain <code>es</code> and <code>id</code> as a minimum.
   * Response: application/json
   
   ```json
   { "dynamoId": "the id of the created/updated item", "es": "The type of the item" }
   ```

Delete
-----
Physically deletes an item. The item is identified by the entity schema name (<code>entityName</code>) and item's identifier (<code>dynamoId</code>).

   * Method: DELETE
   * URL: /delete
   * Query string: entityName=<code>entityName</code>&dynamoId=<code>dynamoId</code>
   * Response: application/json
   
```json
    {
        "deleted": "true"
    }
```

GetById
----------------
Returns an item with the specified entityName and dynamoId.

   * Method: GET
   * URL: /getbyid
   * Query string: entityName=<code>entityName</code>&dynamoId=<code>dynamoId</code>
   * Header: <code>x-columns</code> a comma-separated list of properties to return.
   * Response: application/json                      

```json
    {
        "id": "the id of the requesed item",
        "es": "The type of the requesed item"
    }
```
    
   Followed by the rest of the requested property/value pairs. 
   
GetByTemplate
----------------
Returns item(s) that match specific template. The items are identified by one or more property/value pairs. 

   * Method: GET
   * URL: /getbytemplate?&page=<code>int|default = -1 returns all</code> 
   * Query string: Stringified JSON object that must contain one or more pairs of property/value.
   * Header: <code>x-columns</code> a comma-separated list of properties to return. 
   * Response: application/json
   
```json
    {
        "Items": [{...}, ...],
        "Next": text | <url to the next page>,
        "IsLast": bool,
        "CurrentCount": int,
        "CurrentPage": int,
        "TotalCount": int              
    }
```

GetView
--------
Returns list of DynamoItems in json or zipped json.

   * Method: GET
   * URL: /GetView?viewPath=<code>text/path of the view</code>&asZip=<code>bool/true|false</code>&page=<code>int/page number</code>
   * Response: json or zipped json.
```json
    {
        "Items": [{...}, ...],
        "Next": text | <url to the next page>,
        "IsLast": bool,
        "CurrentCount": int,
        "CurrentPage": int,
        "TotalCount": int              
    }
```

GetViewSQL
--------
Returns list of DynamoItems in json or zipped json.

   * Method: GET
   * URL: /GetView?viewName=<code>text/name of the view</code>&page=<code>int/page number</code>&asZip=<code>bool/true|false</code>&orderByColumn=<code>text/column to order by</code>
   * Response: json or zipped json.
```json
    {
        "Items": [{...}, ...],
        "Next": text | <url to the next page>,
        "IsLast": bool,
        "CurrentCount": int,
        "CurrentPage": int,
        "TotalCount": int              
    }
```

UploadFiles
--------
Returns a file for the specified document id. 

   * Method: POST
   * URL: /UploadFiles
   * Response: json.
```json
    {
        "Status"="Ok|Error",
        Data="<empty>|<error_message>"
    }
```
   * Body: multipart encoded filenames
   * Note: be sure to specify filenames

GetDocument
--------
Returns a file for the specified document id. 

   * Method: GET
   * URL: /GetDocument?id=<code>document id</code>
   * Response: the file

GetDocuments
--------
Returns a zip file for the specified document ids(semicolon separated). 

   * Method: GET
   * URL: /GetDocuments?docIds=<code>document ids</code>
   * Response: the zip file

SearchDocuments
--------
Returns a zip file for the specified document ids(semicolon separated). 

   * Method: GET
   * URL: /SearchDocuments?query=<code>text/next token</code>
   * Response: application/json
      * Response is paged and contains maximum of 10 matches.

Less than 10 matches:
```json
    {
        "next": null,
        "items": [{
            "Id": "0000000-0000-0000-0000-000000000000",
            "Highlights": [
                " you at the <em>event</em>.\n\n"
            ],
            "Title": "Event RSVP Letter"
        }]
    }
```

More than 10 matches:
```json
    {
        "next": "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuv|wxyz0123456789-_ABCDEFGHIJKLMNOPQRSTUVWXYZa",
        "items": [{
            "Id": "0000000-0000-0000-0000-000000000000",
            "Highlights": [
                " you at the <em>event</em>.\n\n"
            ],
            "Title": "Event RSVP Letter"
        },
        ...]
    }
```

Error:
```json
    {
        "err": true,
        "errMessage": "Exception message if any"
    }
```

Each item found in the search results represents a matching document in Dynamo.
If you don't see the document you need, you may not have permissions to view it.

Use <code>Id</code> to download the document with GetDocuments.

<code>Highlights</code> contains snippets of the document body and shows the matching search terms in &lt;em&gt; tag.

Execute Command
--------
Executes a command/workflow logic defined in Dynamo. 

   * Method: POST
   * URL: /executecommand
   * Body: Stringified JSON object that must contain <code>commandName</code> and one or more pairs of property/value.
   * Response: application/json
   
   ```json
   { "err": "true/false", "errMessage": "Exception message if any" }
   ```

# MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

No conditions.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


