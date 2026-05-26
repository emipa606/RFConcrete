# GitHub Copilot Instructions for [RF] Concrete (Continued) Mod

## Mod Overview and Purpose

**[RF] Concrete (Continued)** is a RimWorld mod that introduces concrete as a versatile building material. It provides players with additional construction options by bringing realistic concrete-related mechanics into RimWorld, allowing for more nuanced base designs. Originally designed as an add-on for the "Fertile Fields" mod, this continuation enables independent functionality while enhancing the in-game economy balance and introducing new construction features.

## Key Features and Systems

- **Concrete Production:** Adds the ability to produce concrete using sand, crushed rocks, and cement, which are all obtained from stone chunks or blocks using the stonecutting table.
- **Embrasure Overhaul:** Includes embrasures crafted from various materials. These structures allow ranged attacks while acting as vents, affecting indoor temperatures and item deterioration.
- **New Wall Types:** Players can construct poured concrete walls, steel-reinforced walls, and plasteel-reinforced concrete walls, offering increasing levels of strength and security.
- **Material Economy Adjustments:** Vanilla floors now require concrete instead of steel, enhancing realism and resource management.
- **Inter-Mod Compatibility:** Offers compatibility and integration with mods like "Fertile Fields," "Advanced Bridges," "Glass+Lights," and "Dub's Skylights," incorporating their resources into the concrete production chain.

## Coding Patterns and Conventions

- **Naming Conventions:** Follow C# standard naming conventions for classes (PascalCase) and methods (PascalCase). Use clear, descriptive names that convey function and intention.
- **Code Structure:** Use static classes for utility functions, and maintain separation of concerns by organizing code logically across multiple classes and files.
- **Comments:** Include inline comments to describe complex logic and important mechanics, particularly where there are interactions with other mods or intricate calculations.

## XML Integration

- **Defs and Patches:** Use XML files to define new item traits, material properties, and recipes. Ensure that all newly introduced elements have unique identifiers within their defName attributes to avoid conflicts.
- **Localization:** Include language files for translations, such as the added Russian translation. Ensure all translatable strings are in separate XML files under the appropriate languages subdirectory.

## Harmony Patching

- **Patch Application:** Implement Harmony patches to modify existing game code behavior without altering the original source. This ensures better compatibility with other mods and updates.
- **Target Methods:** Use Harmony patches to target specific vanilla methods that the mod aims to change or extend, focusing on areas like item spawning, construction, and deconstruction.
- **Error Handling:** Implement exception handling within patched methods to prevent disruptions caused by unforeseen interactions or mod-specific errors.

## Suggestions for GitHub Copilot

- **Simplified Queries:** When seeking assistance, provide specific comments within code blocks to receive more targeted suggestions from Copilot.
- **Pattern Completion:** For repetitive code structures, use incomplete method signatures or class definitions to prompt Copilot for completion based on similar existing patterns.
- **XML Assistance:** Use Copilot to auto-generate XML structure templates by providing schema outlines.
- **Debugging:** Ask Copilot for help with potential debugging strategies or to optimize existing code logic for performance improvements.
- **Inter-Mod Dependencies:** Request Copilot suggestions for integrating new features with external mods to ensure seamless compatibility.

By following these instructions, contributions to the [RF] Concrete (Continued) mod will maintain a high standard of quality, coherence, and compatibility, ensuring a smooth experience for users and developers alike.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
