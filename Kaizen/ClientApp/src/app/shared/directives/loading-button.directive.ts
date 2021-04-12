import {
  ComponentFactory,
  ComponentFactoryResolver,
  ComponentRef,
  Directive,
  ElementRef,
  HostBinding,
  Input,
  OnChanges,
  Renderer2,
  SimpleChanges,
  ViewContainerRef
} from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { MatProgressSpinner } from '@angular/material/progress-spinner';

@Directive({
  selector: '[appLoadingButton]'
})
export class LoadingButtonDirective implements OnChanges {
  @HostBinding('disabled')
  @Input('appLoadingButton')
  loading: boolean;

  @Input() color: ThemePalette = 'primary';

  private readonly spinnerFactory: ComponentFactory<MatProgressSpinner>;
  private spinner: ComponentRef<MatProgressSpinner>;

  constructor(
    private elementRef: ElementRef<HTMLButtonElement>,
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private rendered: Renderer2
  ) {
    this.spinnerFactory = this.componentFactoryResolver.resolveComponentFactory(MatProgressSpinner);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes.loading) {
      return;
    }

    if (changes.loading.currentValue) {
      this.elementRef.nativeElement.classList.add('mat-loading');
      this.createSpinner();
    } else if (!changes.loading.firstChange) {
      this.elementRef.nativeElement.classList.remove('mat-loading');
      this.destroySpinner();
    }
  }

  private createSpinner(): void {
    if (!this.spinner) {
      this.spinner = this.viewContainerRef.createComponent(this.spinnerFactory);
      this.spinner.instance.diameter = 20;
      this.spinner.instance.color = this.color;
      this.spinner.instance.mode = 'indeterminate';
      this.rendered.appendChild(this.elementRef.nativeElement, this.spinner.instance._elementRef.nativeElement);
      this.rendered.setProperty(this.elementRef.nativeElement, 'disabled', true);
    }
  }

  private destroySpinner(): void {
    if (this.spinner) {
      this.spinner.destroy();
      this.spinner = null;
    }
  }
}
