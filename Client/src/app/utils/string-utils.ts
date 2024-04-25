export interface SuccessResult<T> {
    success: true,    
    value: T
}

export interface ErrorResult {
    success: false,
    errorMessage: string
}

export type OperationResult<T> = SuccessResult<T> | ErrorResult

export function testPattern(value: string): OperationResult<boolean> {
    const pattern = new RegExp('^[0-9\\s]{1,}((,)|(,[0-9]{1,2}))?\\s*$');
    const match = pattern.test(value);
    if (match) {
        return {
            success: true,
            value: true
        }
    }
    return {
        success: false,
        errorMessage: 'Value must contain only numbers, spaces and a comma separator. '
            + 'Only two digits after comma are allowed.'
    }
}

export function toNumber(value: string): OperationResult<number> {
    value = value.replaceAll(' ','');

    let [integers, hundreths, ...rest] = value.split(',');
    const integersPart = Number.parseInt(integers);

    // Cover the case when '0.10' becomes 1 instead of 10
    hundreths = hundreths?.length === 1 ? hundreths + '0' : hundreths;

    const hundrethsPart = Number.parseInt(hundreths || '0');

    if (isNaN(integersPart) || isNaN(hundrethsPart)) {
        return {
            success: false,
            errorMessage: 'Provided value seems incorrect, please check formatting'
        };
    }

    const parsedValue = integersPart + hundrethsPart / 100;
    if (integersPart > 999_999_999) {
        return {
            success: false,
            errorMessage: 'Value must be smaller than or equal to 999999999.99'
        };
    }
    return {
        success: true,
        value: parsedValue
    }
}