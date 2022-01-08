# from https://www.ef.com/ca/english-resources/english-vocabulary/top-3000-words/
def get_common_words(length=0):
    with open('data/common.txt', 'r') as f:
        words = [w.strip().lower() for w in f.readlines() if w]
    if length:
        return [w for w in words if len(w) == length]
