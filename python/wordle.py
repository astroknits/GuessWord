from enum import Enum
import random
from termcolor import colored, cprint
import string

from argparse import ArgumentParser

from english_words import english_words_lower_alpha_set
from nltk.corpus import words
from common import get_common_words

WORD_LENGTH = 5
TRIES = 6

class LetterState(Enum):
    UNSELECTED = 0
    NO = 1
    YES = 2
    IN_POSITION = 3


class Word(object):
    def __init__(self, solution, valid_words):
        self.word_length = len(solution)
        self.solution = solution
        self.valid_words = valid_words
        self.value = None
        self.match = [LetterState.NO]*self.word_length

    def guess(self, word):
        word = word.lower()
        if not self.is_valid(word):
            raise ValueError(f'{word} is not in the word list')
        self.set_word(word)

    def set_word(self, word):
        self.value = word
        self.check_match()

    def check_match(self):
        for pos, letter in enumerate(self.value):
            if letter not in self.solution:
                self.match[pos] = LetterState.NO
                continue
            if self.solution[pos] == letter:
                self.match[pos] = LetterState.IN_POSITION
                continue
            self.match[pos] = LetterState.YES

    def is_valid(self, word):
        if len(word) != self.word_length:
            return False
        return word in self.valid_words

    def print_word(self):
        if not self.value:
            print(' '.join(['_' for i in range(self.word_length)]))
            return
        print(' '.join([Grid.print_color(letter, state) for letter, state in zip(self.value, self.match)]))



class Grid(object):
    def __init__(self, word_length=WORD_LENGTH, tries=TRIES):
        self.word_length = word_length
        self.common_words = self.get_common_words()
        self.valid_words = self.get_valid_words()

        # self.solution = 'mille'
        self.solution = self.generate_solution()
        print(f'solution: {self.solution}\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n')
        self.tries = [self.get_word() for t in range(tries)]
        self.alphabet = {l:LetterState.UNSELECTED for l in list(string.ascii_lowercase)}

    def get_common_words(self):
        # common words, for choosing the solution
        return get_common_words(length=self.word_length)

    def get_valid_words(self):
        all_words = set(words.words()).union(english_words_lower_alpha_set).union(set(self.common_words))
        valid_words = list([w.lower().strip() for w in all_words if len(w) == self.word_length])
        valid_words += list([f'{w.lower()}s' for w in all_words if len(w) == self.word_length-1])
        return valid_words

    def generate_solution(self):
        return self.common_words[int(random.uniform(0, len(self.common_words)))]

    def get_word(self):
        return Word(self.solution, self.valid_words)

    def get_tries(self):
        return len([t for t in self.tries if t.value])

    @staticmethod
    def print_color(letter, state):
        color_map = {LetterState.UNSELECTED: 'white',
                     LetterState.NO: 'red',
                     LetterState.YES: 'cyan',
                     LetterState.IN_POSITION: 'green'}
        return colored(letter, color_map[state])

    def print_alphabet(self):
        keyboard = [['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'],
                    ['a', 's', 'd', 'f', 'g', 'h','j', 'k', 'l'],
                    ['z', 'x', 'c', 'v', 'b', 'n', 'm']]
        for row in keyboard:
            print(' '.join([self.print_color(letter, self.alphabet[letter]) for letter in row]))
        print('\n')

    def update_alphabet(self, try_word):
        for letter, state in zip(try_word.value, try_word.match):
            self.alphabet[letter] = state

    def print_grid(self):
        print('---------------')
        for guess in self.tries:
            guess.print_word()
        print('---------------\n')
    
    def make_guess(self, i):
        self.print_grid()
        self.print_alphabet()
        word = input(f'Input guess #{i+1}\n\n')
        try:
            try_word = self.tries[i]
            try_word.guess(word)
            self.update_alphabet(try_word)
            if word == self.solution:
                return True
        except ValueError:
            print(f'Boo, {word} is not in the word list.  Please try again.\n')
            if self.make_guess(i):
                return True
        return False

    def run(self):
        win = False
        for i in range(len(self.tries)):
            print('######################')
            if self.make_guess(i):
                print('make_guess is TRUE')
                win = True
                break
        self.print_grid()
        print(f'solution was {self.solution}')
        if win:
            print('You win!!!')

def parse_args():
    parser = ArgumentParser()
    parser.add_argument('-w', '--word-length', type=int, default=WORD_LENGTH, help='Length of the words to guess')
    parser.add_argument('-n', '--num-tries', type=int, default=TRIES, help='Number of guesses allowed')
    return parser.parse_args()
    
def main():
    args = parse_args()
    grid = Grid(word_length=args.word_length, tries=args.num_tries)
    grid.run()

if __name__ == '__main__':
    main()
