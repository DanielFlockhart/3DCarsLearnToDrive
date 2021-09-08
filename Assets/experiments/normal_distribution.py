import matplotlib.pyplot as plt
import random,math
def sample(mean = 0.9,sd=0.2):
    x1 = 1 - random.random()
    x2 = 1 - random.random()
    y1 = math.sqrt(-2.0 * math.log(x1)) * math.cos(2.0 * math.pi * x2);
    value =  y1 * sd + mean;
    return 0 if value < 0 else min(1,value);

data = [sample() for x in range(10000)]
plt.hist(data)
plt.show()
