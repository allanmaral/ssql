## Super Simple Query Language Grammar

```
query: function;

function: IDENTIFIER '(' expressionList ')';
    
expressionList: expression (',' expression)*;

expression:
    literal
    | name
    | function
              
literal:
    STRING
    | INT
    | FLOAT
    | 'true'
    | 'false';

name: IDENTIFIER ('.' IDENTIFIER)*;    
```

### Examples

```
and(eq(firstName, "foo"), eq(lastName, "bar"))
```

```
and(gt(publishedYear, 2000), lt(publishedYear, 2015))
```

```
or(in(status, "published", "pendingApproval"), gte(createdAt, "2022-01-01T00:00:00Z"))
```