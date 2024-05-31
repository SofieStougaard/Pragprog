import numpy as np
import scipy.integrate as spi

# Function definitions
def inv_sqrt(x):
    return 1 / np.sqrt(x)

def ln_inv_sqrt(x):
    return np.log(x) / np.sqrt(x)

# Wrapper to count function evaluations
class EvaluationCounter:
    def __init__(self, func):
        self.func = func
        self.count = 0

    def __call__(self, x):
        self.count += 1
        return self.func(x)

# Compare integrals using scipy's quad method
def compare_integrals():
    # Create evaluation counters for functions
    inv_sqrt_counter = EvaluationCounter(inv_sqrt)
    ln_inv_sqrt_counter = EvaluationCounter(ln_inv_sqrt)

    # Integrate using scipy
    result_inv_sqrt, error_inv_sqrt = spi.quad(inv_sqrt_counter, 0, 1)
    result_ln_inv_sqrt, error_ln_inv_sqrt = spi.quad(ln_inv_sqrt_counter, 0, 1)

    # Print results
    print(f"Integral of 1/sqrt(x) from 0 to 1:")
    print(f"  Result: {result_inv_sqrt}, Expected: 2.0, Evaluations: {inv_sqrt_counter.count}")
    print(f"Integral of ln(x)/sqrt(x) from 0 to 1:")
    print(f"  Result: {result_ln_inv_sqrt}, Expected: -4.0, Evaluations: {ln_inv_sqrt_counter.count}")

if __name__ == "__main__":
    compare_integrals()

