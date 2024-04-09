# cadflash
A .NET MAUI Flashcard Application tailored to C# programming knowledge

![Home Screen](https://github.com/strataghast/cadflash/assets/122714358/0bf4f50e-82ea-4fa3-8831-ef424645dd2a)

This was my first .NET MAUI project that implements various features, including SQLite databases, EntityFrameworkCore, and MVVM. I submitted this for my final project for the Microsoft Software and Systems Academy (MSSA). 

![Difficulty Selection](https://github.com/strataghast/cadflash/assets/122714358/685be69e-9e95-4549-9bf4-dcb54a2ca296)

The application breaks the difficulty levels into three separate categories (Basics, Intermediate, and Advanced). The difficulty screen also shows your current score streak while using the application. You can also utilize basic CRUD operations to modify the flashcard database (create new flashcards, update existing ones, or delete them).

The CRUD view also displays your current score streak while the application is in use, as well as the current high score streak.

![CRUD Menu](https://github.com/strataghast/cadflash/assets/122714358/c91c10ad-eeb4-43f8-911f-2bf2e862693f)

For studying flashcards, I incorporated a simple layout that includes a 'turn' button, which utilizes a function to call a random flashcard filtered to a specific diffulty level from the SQLite database. You can turn the flashcard between the question and the answer as you choose, and move to the next random flashcard. You can also utilize the '✓' button to increment how many flashcards you get right, while the '✕' button stops the current score streak, compares to the current high score database, and replaces it if higher.

![Study](https://github.com/strataghast/cadflash/assets/122714358/d350fd5b-73f6-44c4-becc-1c5728d289b0)


Although I created this application for turn-in as my final project for MSSA, I also wanted a useful tool to practice programming foundations for preparation to transition into the Tech industry.

