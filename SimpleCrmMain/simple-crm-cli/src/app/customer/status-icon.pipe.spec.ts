import { TestBed } from '@angular/core/testing';
import { StatusIconPipe } from './status-icon.pipe';

describe('StatusIconPipe', () => {

  it('create an instance', () => {
    const pipe = new StatusIconPipe();
    expect(pipe).toBeTruthy();
  });
  it('Prospect should result in onlune', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Prospect');
    expect(x).toEqual('online');
  });
  it('Prospect(lowercase) should result in onlune', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('prospect');
    expect(x).toEqual('online');
  });
  it('prOspEct (mixed case) should result in onlune', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('prOspEct');
    expect(x).toEqual('online');
  });
  it('Purchased should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Purchased');
    expect(x).toEqual('money');
  });
  it('purchased (lowercase) should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('purchased');
    expect(x).toEqual('money');
  });
  it('pUrchased (mixed case) should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('pUrchased');
    expect(x).toEqual('money');
  });
  it('empty string should result in fallback value', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('');
    expect(x).toEqual('users');
  });
  it('null should result in fallback value', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('null');
    expect(x).toEqual('users');
  });
});
