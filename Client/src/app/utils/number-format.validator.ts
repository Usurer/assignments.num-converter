import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { testPattern, toNumber } from "./string-utils";

export function numberFormatValidator(): ValidatorFn {
    return (control: AbstractControl<string>): ValidationErrors | null => {
    
        if (control.value.trim().length === 0) {
            return {
                customPattern: 'Value is required'
            }
        }
    
        const patternResult = testPattern(control.value)
        if (!patternResult.success) {
            return {
                customPattern: patternResult.errorMessage,
            };
        }

        const parseResult = toNumber(control.value);
        if(!parseResult.success) {
            return {
                customPattern: parseResult.errorMessage
            }
        }

        return null;
    };
  }