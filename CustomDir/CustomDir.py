import os

print('+', os.getcwd(), sep='')

for root, dirs, files in os.walk('.'):
    for name in files:
        depth = root.count(os.path.sep) + 1
        print(depth*' ', name, sep='')
    for name in dirs:
        depth = root.count(os.path.sep) + 1
        print(depth*' ', '+', name, sep='')