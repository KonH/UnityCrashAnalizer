# UnityCrashAnalizer

Simple utility to extract useful data from Unity Android native crash logs.

## Requirements

* .NET Core
* Android NDK

## Limitations

* Only arm architecture is supported

## Analyze process

1. Get crash dump from device logs
2. Get APK file for that crash dump
3. Find suitable objdump ulitity in NDK prebuilt directory
4. Run CrashAnalyzer project with arguments -apk "....apk" -dump "....txt" -objdump ".../arm-linux-androideabi-objdump" -bundle "apk_bundle_id"
5. View generated .summary.txt file in crash dump location
