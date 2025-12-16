"""
Given code snippet
import random
def fun(s):
    n=random.randint(1, s)
    return n


def main():
    s=6
    r1=True
    while r1:
        r2=input("Ready to roll? Enter Q to Quit")
        if r2.lower() !="q":
            n=fun(s)
            print("You have rolled a",n)
        else:
            r1=False
""""

# Refactored code with meaningful names
import random

def roll_dice(number_of_sides):
    random_rolled_value = random.randint(1, number_of_sides)
    return random_rolled_value


def main():
    dice_total_sides = 6
    is_rolling = True

    while is_rolling:
        user_input = input("Ready to roll? Enter Q to Quit: ")

        if user_input.lower() != "q":
            dice_value = roll_dice(dice_total_sides)
            print("You have rolled a", dice_value)
        else:
            is_rolling = False


main()
