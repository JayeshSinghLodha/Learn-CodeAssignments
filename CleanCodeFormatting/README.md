# Clean Code Formatting Assignment

## Problem
Original `PaymentProcessor` had correct logic but poor formatting.

## Changes Applied

**Horizontal Formatting:**
- Fixed indentation (4 spaces)
- Added spacing around operators and commas
- Broke long lines (max 120 chars)

**Vertical Formatting:**
- Applied newspaper metaphor (high-level first)
- Added blank lines between sections
- Ordered: caller before callee

**Code Structure:**
Constants -> Fields -> Constructor -> Public methods -> Private helpers

## Before/After Example
```java
// Before
private Logger logger;private NotificationService notifier;
public PaymentProcessor(Logger logger,NotificationService notifier){
this.logger=logger;this.notifier=notifier;}

// After
private Logger logger;
private NotificationService notifier;

public PaymentProcessor(Logger logger, NotificationService notifier) {
    this.logger = logger;
    this.notifier = notifier;
}
```

**Logic unchanged** - Only formatting improved
