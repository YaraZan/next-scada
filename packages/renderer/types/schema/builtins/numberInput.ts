import type { Node, NodeAspect, NodeProperties, NodeProperty, NodeStyles } from '../node';

type NumberInputPropertyKeys = 'value';
type NumberInputProperties = Record<NumberInputPropertyKeys, NodeProperty> & NodeProperties;
type NumberInputStyleKeys = 'placeholder' | 'color';
type NumberInputStyles = Record<NumberInputStyleKeys, NodeAspect> & NodeStyles;

export interface NumberInput extends Node {
  properties?: NumberInputProperties;
  styles?: NumberInputStyles;
}
