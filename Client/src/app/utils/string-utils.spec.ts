import { SuccessResult, testPattern, toNumber } from "./string-utils";

describe('String utils', () => {
    
    describe('testPattern', () => {
        it('should match string with spaces as separators', () => {
            const value = '999 999,9';
            const result = testPattern(value);
            expect(result.success).toBeTrue();
        })

        it('should match string with comma and without decimals', () => {
            const value = '999 999,';
            const result = testPattern(value);
            expect(result.success).toBeTrue();
        })

        it('should not match string with more than 2 decimals', () => {
            const value = '999 999,123';
            const result = testPattern(value);
            expect(result.success).toBeFalse();
        })

        it('should not match string with letters', () => {
            const value = '99abc 999,123';
            const result = testPattern(value);
            expect(result.success).toBeFalse();
        })
    })

    describe('toNumber', () => {
        it('Should parse string with spaces', () => {
            const value = ' 123 345,78 ';
            const result = toNumber(value);
            expect(result.success).toBeTrue();
        });

        it('Should not parse string without integer part', () => {
            const value = ' ,78 ';
            const result = toNumber(value);
            expect(result.success).toBeFalse();
        });

        it('Should not parse string with number greater then 999_999_999', () => {
            const value = '1 000 000 000';
            const result = toNumber(value);
            expect(result.success).toBeFalse();
        });

        it('Should parse `1,1` as 1.10 not as 1.01', () => {
            const value = '0,1';
            const result = toNumber(value) as SuccessResult<number>;
            expect(result.value).toEqual(0.10);
        });
    })
})