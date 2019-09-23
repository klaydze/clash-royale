import { AbstractControl, ValidatorFn } from '@angular/forms';

/**
 * Numeric validator class
 */
export class NumberValidators {
    /**
     * Number range validator
     */
    static range(min: number, max: number): ValidatorFn {
        return (c: AbstractControl): { [key: string]: boolean } | null => {
            if (c.value && (isNaN(c.value) || c.value < min || c.value > max)) {
                return { 'range': true };
            }
            return null;
        };
    }
}
