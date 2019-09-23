import { Component, OnInit, ElementRef, ViewChildren } from '@angular/core';
import { FormGroup, FormBuilder, FormControlName, Validators } from '@angular/forms';
import { GenericFormValidator } from 'src/app/shared/generic-form-validator';
import { Observable, fromEvent, merge } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NumberValidators } from 'src/app/shared/number-validator';
import { Card } from 'src/app/cards/card';
import { CardService } from 'src/app/cards/card.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  cardForm: FormGroup;
  card: Card;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private formValidationMessages: { [key: string]: { [key: string]: string } };
  private genericFormValidator: GenericFormValidator;

  constructor(private _formBuilder: FormBuilder,
    private _cardService: CardService,
    private router: Router, ) {
    // Defines all of the validation messages for the form.
    // These could instead be retrieved from a file or database.
    this.formValidationMessages = {
      idName: {
        required: 'Card name is required.',
        minlength: 'Card name must be at least three characters.',
        maxlength: 'Card name cannot exceed 50 characters.'
      },
      name: {
        required: 'Card name is required.',
        minlength: 'Card name must be at least three characters.',
        maxlength: 'Card name cannot exceed 50 characters.'
      },
      rarity: {
        required: 'Please select rarity.'
      },
      type: {
        required: 'Please select type.'
      },
      elixirCost: {
        required: 'Elixir cost is required.',
        range: 'Elixir cost must be between 0 (lowest) and 100 (highest)'
      },
      version: {
        required: 'Version is requried.',
        min: 'Minimum value is 0.'
      },
      count: {
        required: 'Count is requried.',
        min: 'Minimum value is 0.'
      },
      deployTime: {
        required: 'Deploy time is required.',
        min: 'Minimum value is 0.'
      },
      hitSpeed: {
        required: 'Hit speed is required.',
        min: 'Minimum value is 0.'
      },
      lifeTime: {
        required: 'Life time is required.',
        min: 'Minimum value is 0.'
      },
      projectileRange: {
        required: 'Projectile range is required.',
        min: 'Minimum value is 0.'
      },
      radius: {
        required: 'Radius is required.',
        min: 'Minimum value is 0.'
      }
    };

    // Define an instance of the validator for use with this form,
    // passing in this form's set of validation messages.
    this.genericFormValidator = new GenericFormValidator(this.formValidationMessages);
  }

  ngOnInit() {
    this.cardForm = this._formBuilder.group({
      idName: ['', [Validators.required, Validators.minLength(3)]],
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: '',
      rarity: ['', [Validators.required]],
      type: ['', [Validators.required]],
      elixirCost: ['', [Validators.required, NumberValidators.range(0, 100)]],
      version: ['', [Validators.required, Validators.min]],
      count: ['', [Validators.required, Validators.min]],
      deployTime: ['', [Validators.required, Validators.min]],
      hitSpeed: ['', [Validators.required, Validators.min]],
      lifeTime: ['', [Validators.required, Validators.min]],
      projectileRange: ['', [Validators.required, Validators.min]],
      radius: ['', [Validators.required, Validators.min]]
    });

    if (this.cardForm) {
      this.cardForm.reset();
    }

    if (!this.card) {
      this.card = this.initializeCard();
    }

    this.cardForm.patchValue({
      idName: this.card.idName,
      name: this.card.name,
      description: this.card.description,
      rarity: this.card.rarity,
      type: this.card.type,
      elixirCost: this.card.elixirCost,
      version: this.card.version,
      count: this.card.count,
      deployTime: this.card.deployTime,
      hitSpeed: this.card.hitSpeed,
      lifeTime: this.card.lifetime,
      projectileRange: this.card.projectileRange,
      radius: this.card.radius
    });
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    const controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) =>
        fromEvent(formControl.nativeElement, 'blur'));

    // Merge the blur event observable with the valueChanges observable
    merge(this.cardForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericFormValidator.processMessages(this.cardForm);
    }, debounceTime(800));
  }

  onSelectedRarity(evt) {
    console.log(evt.target.value);
  }

  onSelectedType(evt) {
    console.log(evt.target.value);
  }

  saveCardInfo(): void {
    if (this.cardForm.dirty && this.cardForm.valid) {
      const newCardInfo = <Card>Object.assign({}, this.card, this.cardForm.value);

      this._cardService.saveCardInformation(newCardInfo)
        .subscribe(res => {
          console.log(res);
          this.onSaveComplete();
        },
          err => {
            console.error(err);
          })
    }
  }

  private onSaveComplete(): void {
    this.cardForm.reset();
    this.router.navigate(['/secured/dashboard']);
  }

  private initializeCard(): Card {
    return {
      id: 0,
      idName: null,
      rarity: null,
      type: null,
      name: null,
      description: null,
      elixirCost: 0,
      version: 0,
      imageUrl: null,
      targets: null,
      hitSpeed: 0,
      count: 0,
      range: null,
      speed: null,
      radius: 0,
      deployTime: 0,
      lifetime: 0,
      dashRange: null,
      projectileRange: 0,
      cardStatistics: []
    };
  }
}
