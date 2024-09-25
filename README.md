
# Model 

데이터 모델, 상수 같은 서버/클라이언트가 공용으로 사용할 데이터가 포함됩니다.

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

# Core

Model 프로젝트를 참조합니다.

게임 로직이 구현됩니다.

# Console

Model, Core 프로젝트를 참조합니다.

콘솔에서 게임 로직을 테스트 할 수 있는 기능을 제공합니다.

추후 스트레스트 테스트와 시뮬레이션 검증 기능이 추가됩니다.

# Tool

Model 프로젝트를 참조합니다.

게임 개발에 필요한 유틸들을 구현합니다. (맵 생성기, 캐릭터 생성기, 프로토콜 생성기 등)


