
# Model 

## Const

구조체 사용으로 기본으로하고 필드는 const / static readonly 2가지를 용도에 맞게 사용합니다.

컴파일 타임 상수 : const  
런타임 상수 : static readonly  

## Enum 

Enum은 Enum안에 정의되고 Model.Enum 네임스페이스를 가집니다.  
> [!Caution]  
> 네이밍에 Type과 같은 접미사는 붙이지 않습니다.  
> ~~CharacterClassType~~ -> CharacterClass

## Model (Structure)

Model은 기본적으로 Model 네임스페이스를 가집니다.
structure로 정의하는 것을 기본으로 합니다.
