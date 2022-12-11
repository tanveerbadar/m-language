type ::= _int_

identifier ::= [a-z_][a-z0-9_]*

number ::= [1-9][0-9]*

add_op ::= _+_ | _-_

primary_expression ::= identifier | number

additive_expression ::= primary_expression add_op additive_expression | primary_expression

expression ::= additive_expression _;_

declarator ::= identifier | identifier _=_ expr

declarator_list ::= declarator _,_ declarator_list | declarator

declaration ::= type declarator_list _;_

program ::= declaration | expression