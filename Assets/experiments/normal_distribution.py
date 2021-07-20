import numpy as np

def sample(mean=0.8,sd=0.2):
    s = np.random.normal(mean, sd)
    return 0 if s < 0 else min(1,s)