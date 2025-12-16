"""
Given code snippet
def fun(s):
    if s.isdigit() and 1<= int(s) <=100:
        return True
    else:
        return False

def main():
    n=random.randint(1,100)
    gn=False
    g=input("Guess a number between 1 and 100:")
    ng=0
    while not gn:
        if not fun(g):
            g=input("I wont count this one Please enter a number between 1 to 100")
            continue
        else:
            ng+=1
            g=int(g)

        if g<n:
            g=input("Too low. Guess again")
        elif g>n:
            g=input("Too High. Guess again")
        else:
            print("You guessed it in",ng,"guesses!")
            gn=True


main()
""""

# Refactored code with meaningful names
import random

def is_valid_guess(user_input):
    if user_input.isdigit() and 1 <= int(user_input) <= 100:
        return True
    else:
        return False


def main():
    secret_number = random.randint(1, 100)
    has_guessed_correctly = False
    guess_input = input("Guess a number between 1 and 100: ")
    number_of_guesses = 0

    while not has_guessed_correctly:
        if not is_valid_guess(guess_input):
            guess_input = input("I won't count this one. Please enter a number between 1 and 100: ")
            continue
        else:
            number_of_guesses += 1
            guessed_number = int(guess_input)

        if guessed_number < secret_number:
            guess_input = input("Too low. Guess again: ")
        elif guessed_number > secret_number:
            guess_input = input("Too high. Guess again: ")
        else:
            print("You guessed it in", number_of_guesses, "guesses!")
            has_guessed_correctly = True


main()