grammar SuperSimpleQueryLanguage;

query: function;

function: IDENTIFIER L_PAREN expressionList R_PAREN;
    
expressionList: expression (COMMA expression)*;

expression
    : literal
    | name
    | function
    ;
              
literal
    : STRING_LIT
    | DECIMAL_LIT
    | FLOAT_LIT
    | TRUE_LIT
    | FALSE_LIT
    ;

name: IDENTIFIER (DOT IDENTIFIER)*;

/*
 * Lexer
 */

L_PAREN: '(';
R_PAREN: ')';
COMMA: ',';
DOT: '.';

IDENTIFIER: LETTER (LETTER | UNICODE_DIGIT)*;

TRUE_LIT: 'true';

FALSE_LIT: 'false';

DECIMAL_LIT: SIGN? [0-9]+;

FLOAT_LIT: SIGN? DECIMALS ('.' DECIMALS? EXPONENT? | EXPONENT)
    | '.' DECIMALS EXPONENT?
    ;

STRING_LIT : '"' (~["\\] | ESCAPED_VALUE)*  '"';

fragment LETTER
    : UNICODE_LETTER
    | '_';

fragment UNICODE_LETTER: [\p{L}];

fragment UNICODE_DIGIT: [\p{Nd}];

fragment ESCAPED_VALUE: '\\' [abfnrtv\\'"];

fragment DECIMALS
    : [0-9] ('_'? [0-9])*
    ;

fragment EXPONENT
    : [eE] [+-]? DECIMALS
    ;

fragment SIGN
    : '+'
    | '-'
    ;
