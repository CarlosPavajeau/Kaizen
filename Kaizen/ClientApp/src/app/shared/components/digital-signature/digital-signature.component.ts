import { Component, OnInit, ViewChild, AfterContentInit, ElementRef, Input, HostListener } from '@angular/core';
import { fromEvent } from 'rxjs';
import { switchMap, takeUntil, pairwise } from 'rxjs/operators';
import { Point } from '@app/core/models/point';

@Component({
  selector: 'app-digital-signature',
  templateUrl: './digital-signature.component.html',
  styleUrls: [ './digital-signature.component.css' ]
})
export class DigitalSignatureComponent implements AfterContentInit {
  @ViewChild('canvas', { static: true })
  public canvas: ElementRef;

  @Input() public width = 300;
  @Input() public height = 200;

  private cx: CanvasRenderingContext2D;
  private canvasElement: HTMLCanvasElement;
  private _isEmpty = true;

  ngAfterContentInit(): void {
    this.canvasElement = this.canvas.nativeElement;
    this.cx = this.canvasElement.getContext('2d');

    this.canvasElement.width = this.width;
    this.canvasElement.height = this.height;
    this.cx.lineWidth = 3;
    this.cx.lineCap = 'round';
    this.cx.strokeStyle = 'black';
    this.cx.fillStyle = 'white';

    this.captureEvents(this.canvasElement);
  }

  private captureEvents(canvasEl: HTMLCanvasElement) {
    fromEvent(canvasEl, 'mousedown')
      .pipe(
        switchMap((e) => {
          return fromEvent(canvasEl, 'mousemove').pipe(
            takeUntil(fromEvent(canvasEl, 'mouseup')),
            takeUntil(fromEvent(canvasEl, 'mouseleave')),
            pairwise()
          );
        })
      )
      .subscribe((res: [MouseEvent, MouseEvent]) => {
        const rect = canvasEl.getBoundingClientRect();

        const prevPos: Point = {
          x: res[0].clientX - rect.left,
          y: res[0].clientY - rect.top
        };

        const currentPos: Point = {
          x: res[1].clientX - rect.left,
          y: res[1].clientY - rect.top
        };

        this.drawOnCanvas(prevPos, currentPos);
      });

    fromEvent(canvasEl, 'touchstart')
      .pipe(
        switchMap((e) => {
          return fromEvent(canvasEl, 'touchmove').pipe(
            takeUntil(fromEvent(canvasEl, 'touchend')),
            takeUntil(fromEvent(canvasEl, 'touchleave')),
            pairwise()
          );
        })
      )
      .subscribe((res: [TouchEvent, TouchEvent]) => {
        const rect = canvasEl.getBoundingClientRect();

        res[0].preventDefault();
        res[1].preventDefault();

        const prevPos: Point = {
          x: res[0].touches[0].clientX - rect.left,
          y: res[0].touches[0].clientY - rect.top
        };

        const currentPos: Point = {
          x: res[1].touches[0].clientX - rect.left,
          y: res[1].touches[0].clientY - rect.top
        };

        this.drawOnCanvas(prevPos, currentPos);
      });
  }

  private drawOnCanvas(prevPos: Point, currentPos: Point) {
    if (!this.cx) {
      return;
    }

    this.cx.beginPath();

    if (prevPos) {
      this.cx.moveTo(prevPos.x, prevPos.y);
      this.cx.lineTo(currentPos.x, currentPos.y);
      this.cx.stroke();
      this._isEmpty = false;
    }
  }

  getImageData(): string {
    const dataUrl = this.canvasElement.toDataURL('image/png', '720px');
    return dataUrl.split(',')[1];
  }

  get isEmpty(): boolean {
    return this._isEmpty;
  }

  clear(): void {
    if (!this.cx) {
      return;
    }

    this.cx.beginPath();
    this.cx.clearRect(0, 0, this.canvasElement.width, this.canvasElement.height);
    this.cx.fillRect(0, 0, this.canvasElement.width, this.canvasElement.height);
    this._isEmpty = true;
  }
}
