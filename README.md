# Exercise: Classes

Exercise code for the lesson **Exercise: Classes** in the C# Full-Stack Bootcamp on [ask-codey.com](https://www.ask-codey.com).

In this exercise you write six classes from scratch, in the `ClassesExercise` project. The tests in `ClassesExercise.Tests` check that each class has the right properties, constructors and methods, and that they behave correctly.

## Getting started

1. Open `ClassesExercise.slnx` in Visual Studio.
2. Choose **Test > Run All Tests**. Every test fails at first: none of the classes exist yet.
3. Right-click the `ClassesExercise` project, choose **Add > Class...**, and name it `Rectangle.cs`. Start with `namespace ClassesExercise;` at the top, and make the class `public`.
4. Write just enough to pass one test, run the tests again, and repeat.

Or, in Git Bash: create `ClassesExercise/Rectangle.cs` in VS Code, and run `dotnet test`.

## How to read the tables

| Set column | Means | Write it as |
|---|---|---|
| X | anyone can set it | `{ get; set; }` |
| private | only the class can set it | `{ get; private set; }` |
| (empty) | calculated from other properties | `=> ...` or `{ get { ... } }`, with no set |

## 1. Rectangle

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| Width | int | X | X | The rectangle's width |
| Height | int | X | X | The rectangle's height |
| Area | int | X | | The width times the height |

Two constructors: one with no parameters, and one with `int width, int height` that sets the properties.

## 2. Circle

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| Radius | int | X | X | The circle's radius |
| Diameter | int | X | | Twice the radius |
| Area | double | X | | π times the radius squared |
| Circumference | double | X | | 2 times π times the radius |

Two constructors: one with no parameters, and one with `int radius`. Use `Math.PI` for π, and `Math.Pow(number, 2)` or `number * number` to square a number.

## 3. MathTest

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| Score | int | X | X | The points the student scored |
| PossiblePoints | int | X | private | The points the test was out of |
| StudentName | string | X | private | The student's name |
| Percent | int | X | | The score as a whole-number percentage |
| LetterGrade | string | X | | "A" for 90% or more, "B" for 80–89%, "C" for 70–79%, "D" for 60–69%, otherwise "F" |

One constructor, with `int possiblePoints, string studentName`. Hint: `Score` and `PossiblePoints` are `int`s. What happens when a smaller `int` is divided by a larger one?

## 4. Printer

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| Paper | int | X | private | Sheets of paper in the tray |
| Toner | int | X | private | Units of toner left |

One constructor, with `int paper, int toner`. The printer holds at most 500 sheets of paper and 1000 units of toner.

Methods:

- `int Print(int copies)`: each copy uses 1 sheet and 1 unit of toner. Print as many as the paper and toner allow (never going below 0), and return how many were printed. Asking for 0 or fewer copies prints nothing and returns 0.
- `void ReplaceToner()`: fills the toner to 1000.
- `void AddPaper(int sheets)`: adds paper, but never more than 500 sheets in all. Adding 0 or fewer sheets does nothing.

## 5. Elevator

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| CurrentLevel | int | X | private | The floor the elevator is on |
| NumberOfLevels | int | X | private | How many floors the building has |
| DoorIsOpen | bool | X | private | Whether the door is open |

One constructor, with `int numberOfLevels`. A new elevator starts on floor 1, with its door closed.

Methods:

- `void OpenDoor()` and `void CloseDoor()`.
- `void GoUp(int desiredFloor)`: moves up to that floor, but only if the door is closed, the floor is above the current one, and it isn't past the top floor. Otherwise it stays where it is.
- `void GoDown(int desiredFloor)`: the same, downward, never below floor 1.

## 6. Airplane (challenge)

| Property | Type | Get | Set | Description |
|---|---|---|---|---|
| PlaneNumber | string | X | private | The plane's six-character number |
| TotalFirstClassSeats | int | X | private | First class seats on the plane |
| BookedFirstClassSeats | int | X | private | First class seats already booked |
| AvailableFirstClassSeats | int | X | | Total minus booked |
| TotalCoachSeats | int | X | private | Coach seats on the plane |
| BookedCoachSeats | int | X | private | Coach seats already booked |
| AvailableCoachSeats | int | X | | Total minus booked |

One constructor, with `string planeNumber, int totalFirstClassSeats, int totalCoachSeats`.

`bool ReserveSeats(bool forFirstClass, int totalNumberOfSeats)` books that many seats in first class or coach, and returns `true`. If there aren't enough seats left, it books nothing and returns `false`.

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
