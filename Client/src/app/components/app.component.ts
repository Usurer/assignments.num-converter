import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { StateService } from '../services/state.service';
import { tap } from 'rxjs';
import { numberFormatValidator } from '../utils/number-format.validator';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.ShadowDom
})
export class AppComponent {

  constructor(private service: StateService) {
  }

  valueInput = new FormControl<string>('0', {
    validators: [
      numberFormatValidator()
    ],
    nonNullable: true
  });

  form = new FormGroup({
    valueInput: this.valueInput
  });

  state$ = this.service.state$.pipe(tap(state => {
    if (state.isLoading) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }));

  public onSubmit() {
    const val = this.valueInput.value;
    this.service.getData(val);
  }

}
