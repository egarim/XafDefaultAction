;#DropDB XafDefaultActionEasyTest

#Application XafDefaultActionWin
#Application XafDefaultActionBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef XafDefaultActionWin

*Action About...
 
*Action OK

#EndIf

#IfDef XafDefaultActionBlazor
*Action Navigation(My Details)
#EndIf
