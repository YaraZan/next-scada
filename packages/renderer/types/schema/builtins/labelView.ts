import type {Node, NodeAspect, NodeProperties, NodeProperty, NodeStyles} from '../node';

type LabelStyleKeys = 'fontSize' | 'fontWeight' | 'color';
type LabelStyles = Record<LabelStyleKeys, NodeAspect> & NodeStyles;
type LabelPropertyKeys = 'text';
type LabelProperties = Record<LabelPropertyKeys, NodeProperty> & NodeProperties;

export interface LabelView extends Node {
  styles?: LabelStyles;
  properties?: LabelProperties;
}
