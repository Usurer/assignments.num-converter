import { Injectable, OnDestroy } from "@angular/core";
import { BehaviorSubject, EMPTY, Observable, Subject, catchError, take, takeUntil, tap } from "rxjs";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { toNumber } from "../utils/string-utils";

const API_URL = 'http://localhost:5195/api/convertion/';

export interface State {
    error?: {
        title?: string,
        message?: string
    },
    value?: string,
    isLoading: boolean
}

@Injectable({
    providedIn: 'root'
})
export class StateService implements OnDestroy {
    private readonly onDestroySubject = new Subject();
    private readonly onDestroy$ = this.onDestroySubject.asObservable();
    private readonly stateSubject = new BehaviorSubject<State>({isLoading: false});

    public readonly state$ = this.stateSubject.asObservable();

    constructor(private httpClient: HttpClient) {
    }

    public getData(value: string): void {
        this.setState({
            isLoading: true,
            value: undefined,
            error: undefined
        });

        const parseResult = toNumber(value);
        if (!parseResult.success) {
            this.setState({
                isLoading: false,
                value: undefined,
                error: {
                    message: parseResult.errorMessage
                }
            });
            return;
        }

        this.httpClient.get<string>(`${API_URL}${parseResult.value}`).pipe(
            takeUntil(this.onDestroy$),
            tap((response) => this.handleSuccess(response)),
            catchError((errorResponse: HttpErrorResponse) => this.handleError(errorResponse)),
            take(1),
        ).subscribe();
    }

    private handleSuccess(response: string): void {
        this.setState({
            isLoading: false,
            value: response
        })
    }

    private handleError(errorResponse: HttpErrorResponse): Observable<never> {
        const error = {
            title: '',
            message: ''
        };

        if (errorResponse.status === 0) {
            error.title = 'An error occured';
        } else {
            error.title = errorResponse.error?.title;
            error.message = (errorResponse.error?.errors?.value as string[])?.join(', ');
        }

        this.setState({
            isLoading: false,
            error
        });

        return EMPTY;
    }

    private setState(state: State): void {
        this.stateSubject.next({
            ...this.stateSubject.value,
            ...state
        })
    }

    ngOnDestroy(): void {
        this.onDestroySubject.next(1);
    }
}
