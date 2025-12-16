"""
def fun(N):
    # Initializing Sum and Number of Digits
    s = 0
    t = 0

    # Calculating Number of individual digits
    t2 = N
    while t2 > 0:
        t = t + 1
        t2 = t2 // 10

    # Finding Armstrong Number
    t2 = N
    for n in range(1, t2 + 1):
        R = t2 % 10
        s = s + (R ** t)
        t2 //= 10
    return s


# End of Function

# User Input
N2 = int(input("\nPlease Enter the Number to Check for Armstrong: "))

if (N2 == fun(N2)):
    print("\n %d is Armstrong Number.\n" % N2)
else:
    print("\n %d is Not a Armstrong Number.\n" % N2)
"""

# Refactored code with meaningful names
def calculate_armstrong_sum(number):
    # Initializing sum of powered digits and digit count
    armstrong_sum = 0
    digit_count = 0

    # Calculating number of digits
    temporary_number = number
    while temporary_number > 0:
        digit_count += 1
        temporary_number //= 10

    # Calculating Armstrong sum
    temporary_number = number
    for _ in range(1, temporary_number + 1):
        digit = temporary_number % 10
        armstrong_sum += digit ** digit_count
        temporary_number //= 10

    return armstrong_sum


# User Input
input_number = int(input("\nPlease Enter the Number to Check for Armstrong: "))

if input_number == calculate_armstrong_sum(input_number):
    print("\n %d is an Armstrong Number.\n" % input_number)
else:
    print("\n %d is Not an Armstrong Number.\n" % input_number)