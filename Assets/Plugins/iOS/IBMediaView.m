//
//  IBMediaView.m
//  Unity-iPhone
//
//  Created by MMiroslav on 2/3/14.
//
//

#import "IBMediaView.h"

@implementation IBMediaView

- (id)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        // Initialization code
    }
    return self;
}

/*
// Only override drawRect: if you perform custom drawing.
// An empty implementation adversely affects performance during animation.
- (void)drawRect:(CGRect)rect
{
    // Drawing code
}
*/


-(void)didDismissInfobipMediaView:(InfobipMediaView *)infobipMediaView {
    // Unregister as a delegate
    infobipMediaView.delegate = nil;
    
    // Dismiss the Media View from the super view
    [infobipMediaView removeFromSuperview];
}

@end
