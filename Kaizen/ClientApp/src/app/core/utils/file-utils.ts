export const downloadFile = (blob: Blob) => {
  if (blob) {
    const link = document.createElement('a');
    const event = document.createEvent('MouseEvent');

    link.download = `certificado-${ Date.now().toString() }.pdf`;
    link.href = URL.createObjectURL(blob);
    link.dataset.downloadurl = [ 'application/octet-stream', link.download, link.href ].join(':');

    event.initMouseEvent('click', true, false, window, 0, 0,
      0, 0, 0, false, false, false, false,
      0, null);

    link.dispatchEvent(event);
  }
};
